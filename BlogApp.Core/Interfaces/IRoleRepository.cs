
using BlogApp.Dto.Role;

namespace BlogApp.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleDto>> GetRolesForUserAsync(string userId);
    }
}
