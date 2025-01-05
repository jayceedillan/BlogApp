using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Validators
{
    public class CustomRoleValidationAttribute : ValidationAttribute
    {
        public CustomRoleValidationAttribute()
        {
            ErrorMessage = "At least one role must be selected.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var selectedRoles = value as List<string>;

            if (selectedRoles == null || selectedRoles.Count == 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
