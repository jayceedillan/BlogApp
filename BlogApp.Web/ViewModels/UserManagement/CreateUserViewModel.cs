using BlogApp.Dto.Role;
using BlogApp.Dto.User;

namespace BlogApp.Web.ViewModels.UserManagement
{
    public class CreateUserViewModel
    {
        public CreateUserDto User { get; set; }
        public List<RoleDto>? Roles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
