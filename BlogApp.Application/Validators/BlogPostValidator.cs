
using BlogApp.Application.Helper;
using BlogApp.Dto.Blog;
using FluentValidation;

namespace BlogApp.Application.Validators
{
    public class BlogPostValidator : AbstractValidator<BlogPostDto>
    {
        public BlogPostValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            //RuleFor(x => x.BannerImagePath)
            //    .Must(path => string.IsNullOrEmpty(path.ToString()) || FileValidationHelper.IsValidBannerImage(path.ToString() ?? string.Empty))
            //    .WithMessage("Banner image must be a valid JPG, PNG, or GIF file and less than 5MB.");

                //.WithMessage("Banner image must be JPG, PNG, or GIF and less than 5MB.");
        }
    }
}
