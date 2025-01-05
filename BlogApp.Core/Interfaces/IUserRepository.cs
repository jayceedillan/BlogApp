using BlogApp.Core.Entities;

namespace BlogApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task AddRolesToUserAsync(ApplicationUser user, IEnumerable<string> roles);
        Task RemoveRolesFromUserAsync(ApplicationUser user);
    }
}
