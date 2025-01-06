using BlogApp.Application.Services;
using BlogApp.Core.Entities;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Interfaces;
using BlogApp.Dto.Blog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, BlogPostDto>
    {
        private readonly IRepository<BlogPost> _repository;
        private readonly IFileService _fileService;

        public UpdateBlogCommandHandler(IRepository<BlogPost> repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<BlogPostDto> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing blog by ID
            var blog = await _repository.GetByIdAsync(request.blogPostDto.Id);

            if (blog == null)
            {
                throw new NotFoundException(nameof(BlogPostDto), request.blogPostDto.Id);
            }

            string? bannerImageUrl = null;
            if (request.blogPostDto.ImagePath != null)
            {
                //IFormFile formFile = FileHelper.ConvertToIFormFile(request.blogPostDto.BannerImagePath);

                // Upload file to the server using IFileService
                bannerImageUrl = await _fileService.SaveFileAsync(request.blogPostDto.ImagePath, "blog_images");
                blog.BannerImagePath = bannerImageUrl;
            }


            blog.Title = request.blogPostDto.Title;
            blog.Content = request.blogPostDto.Content;

            
            await _repository.UpdateAsync(blog);

            return request.blogPostDto;
        }
    }

}
