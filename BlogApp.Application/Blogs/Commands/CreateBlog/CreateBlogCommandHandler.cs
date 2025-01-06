using BlogApp.Application.Services;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using MediatR;

namespace BlogApp.Application.Blogs.Commands.CreateBlog
{

    namespace BlogApp.Application.Blogs.Commands.CreateBlog
    {
        public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BlogPost>
        {
            private readonly IRepository<BlogPost> _repository;
            private readonly IFileService _fileService;

            public CreateBlogCommandHandler(IRepository<BlogPost> repository, IFileService fileService)
            {
                _repository = repository;
                _fileService = fileService;
            }

            public async Task<BlogPost> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {
                // Validate request fields (additional validation can be done here)
                if (string.IsNullOrWhiteSpace(request.blogPostDto.Title) || string.IsNullOrWhiteSpace(request.blogPostDto.Content))
                {
                    throw new ArgumentException("Title and Content must be provided.");
                }

                // Process the banner image (if provided)
                string? bannerImageUrl = null;
                if (request.blogPostDto.BannerImagePath != null)
                {
                    // Upload file to the server using IFileService
                    bannerImageUrl = await _fileService.SaveFileAsync(request.blogPostDto.BannerImagePath, "blog_images");
                }

                var newBlog = new BlogPost
                {
                    Title = request.blogPostDto.Title,
                    Content = request.blogPostDto.Content,
                    Status = request.blogPostDto.Status,
                    AuthorId = request.blogPostDto.AuthorId, // Assuming request.CreatedBy is the AuthorId
                    Author = null, // You may fetch the Author (ApplicationUser) based on AuthorId if needed
                    BannerImagePath = bannerImageUrl ?? string.Empty,
                    CreatedAt = DateTime.UtcNow,
                    PublishedAt = request.blogPostDto.PublishedAt // This assumes that PublishedAt is set in DTO (if applicable)
                };


                // Add the new blog to the repository
                await _repository.AddAsync(newBlog);
                return newBlog;
            }
        }
    }


}
