
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

            RuleFor(x => x.BannerImagePath)
                .Must(x => x == null || (x.Length <= 5 * 1024 * 1024 &&
                                         (x.ContentType == "image/jpeg" ||
                                          x.ContentType == "image/png" ||
                                          x.ContentType == "image/gif")))
                .WithMessage("Banner image must be JPG, PNG, or GIF and less than 5MB.");
        }
    }
}
