
using BlogApp.Dto.Role;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Dto.User
{
    public class EditUserDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public List<RoleDto>? Roles { get; set; }

        //[CustomRoleValidation(ErrorMessage = "Please select at least one valid role.")]
        public List<string> SelectedRoles { get; set; }
    }
}
