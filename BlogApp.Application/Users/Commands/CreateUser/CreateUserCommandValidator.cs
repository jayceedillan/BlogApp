using FluentValidation;
using BlogApp.Application.Validators;

namespace BlogApp.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserDto.UserName).NotEmpty().Length(3, 50);
            RuleFor(x => x.UserDto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(8, 100).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.");
            RuleFor(x => x.UserDto.SelectedRoles)
                 .Must(roles => roles != null && roles.Count > 0)
                 .WithMessage("Please select at least one valid role.");
        }
    }
}
