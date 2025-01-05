using AutoMapper;
using BlogApp.Core.Interfaces;
using FluentResults;
using MediatR;
using System.Transactions;

namespace BlogApp.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Start a new transaction scope
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Get the user by ID
                    var user = await _userRepository.GetUserByIdAsync(request.UserDto.Id ?? string.Empty);
                    if (user == null)
                    {
                        return Result.Fail("User not found.");
                    }

                    // Map the properties from the DTO to the existing user
                    _mapper.Map(request.UserDto, user);

                    // Update the user data in the repository
                    await _userRepository.UpdateUserAsync(user);

                    // Remove the existing roles before assigning new ones (if roles have changed)
                    if (request.UserDto.SelectedRoles != null && request.UserDto.SelectedRoles.Any())
                    {
                        // Remove current roles (if any) for this user
                        await _userRepository.RemoveRolesFromUserAsync(user);

                        // Add the selected roles to the user
                        await _userRepository.AddRolesToUserAsync(user, request.UserDto.SelectedRoles);
                    }
                    // Complete the transaction
                    transaction.Complete();

                    // Return success with the user ID
                    return Result.Ok(user.Id);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, and transaction will be rolled back automatically
                    return Result.Fail($"Error occurred: {ex.Message}");
                }
            }
        }

    }


}
