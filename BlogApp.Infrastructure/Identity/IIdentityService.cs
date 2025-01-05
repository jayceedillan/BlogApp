using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Infrastructure.Identity
{
    public interface IIdentityService
    {
        Task<bool> IsLastAdminAsync(string userId);
    }

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsLastAdminAsync(string userId)
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            return adminUsers.Count == 1 && adminUsers.First().Id == userId;
        }
    }
}
