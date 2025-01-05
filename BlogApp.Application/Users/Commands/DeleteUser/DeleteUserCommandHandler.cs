using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using BlogApp.Core.Entities;
using BlogApp.Core.Exceptions;
using FluentResults;

namespace BlogApp.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<DeleteUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Fetch the user asynchronously with error handling
            var user = await GetUserByIdAsync(request.UserId);
            if (user == null)
            {
                return Result.Fail<string>($"User with ID {request.UserId} not found.");
            }

            // Step 2: Ensure that the last admin user is not deleted
            var deletionValidationResult = await ValidateDeleteUserIsNotLastAdminAsync(user);
            if (!deletionValidationResult.IsSuccess)
            {
                return deletionValidationResult;
            }

            // Step 3: Proceed with deletion and handle errors
            var deletionResult = await DeleteUserAsync(user);
            if (!deletionResult.IsSuccess)
            {
                return deletionResult;
            }

            // Step 4: Log the successful deletion
            _logger.LogInformation($"User successfully deleted. User ID: {user.Id}, Email: {user.Email}");

            // Return the result with user ID on success
            return Result.Ok(user.Id);
        }

        // Step 1: Fetch the user by ID
        private async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    throw new UserManagementException($"User with ID {userId} not found.");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching user with ID {userId}: {ex.Message}");
                throw new UserManagementException($"Error retrieving user with ID {userId}. Please try again later.");
            }
        }

        // Step 2: Validate that the user is not the last admin
        private async Task<Result<string>> ValidateDeleteUserIsNotLastAdminAsync(ApplicationUser user)
        {
            try
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("Admin"))
                {
                    var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                    if (adminUsers.Count == 1)
                    {
                        return Result.Fail<string>("Cannot delete the last admin user.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error validating if user is the last admin: {ex.Message}");
                throw new UserManagementException("Error during user validation. Please try again later.");
            }

            return Result.Ok("User can be deleted.");
        }

        // Step 3: Attempt to delete the user
        private async Task<Result<string>> DeleteUserAsync(ApplicationUser user)
        {
            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    _logger.LogError($"Failed to delete user. Errors: {string.Join(", ", errors)}");
                    return Result.Fail<string>($"Failed to delete user: {string.Join(", ", errors)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user with ID {user.Id}: {ex.Message}");
                throw new UserManagementException($"An unexpected error occurred while deleting the user {user.Id}. Please try again later.");
            }

            return Result.Ok(user.Id.ToString());
        }

    }

}
