using System.ComponentModel.DataAnnotations;
using BlogApp.Dto.Role;

namespace BlogApp.Dto.User
{
    public class CreateUserDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.")]
        public string Password { get; set; }

        public List<RoleDto>? Roles { get; set; }

        public List<string> SelectedRoles { get; set; }
    }
}