using AutoMapper;
using BlogApp.Application.Services;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogApp.Application.Blogs.Commands.CreateBlog
{

    namespace BlogApp.Application.Blogs.Commands.CreateBlog
    {
        public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BlogPost>
        {
            private readonly IRepository<BlogPost> _repository;
            private readonly IFileService _fileService;
            private readonly IMapper _mapper;

            public CreateBlogCommandHandler(IRepository<BlogPost> repository, IFileService fileService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            {
                _repository = repository;
                _fileService = fileService;
                _mapper = mapper;
            }

            public async Task<BlogPost> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.blogPostDto.Title) || string.IsNullOrWhiteSpace(request.blogPostDto.Content))
                {
                    throw new ArgumentException("Title and Content must be provided.");
                }

                string? bannerImageUrl = null;
                
                if (request.blogPostDto.ImagePath != null)
                {
                    bannerImageUrl = await _fileService.SaveFileAsync(request.blogPostDto.ImagePath, "blog_images");
                }
                var newBlog = _mapper.Map<BlogPost>(request.blogPostDto);
                newBlog.BannerImagePath = bannerImageUrl ?? string.Empty;

                await _repository.AddAsync(newBlog);

                return newBlog;
            }
        }
    }
}
