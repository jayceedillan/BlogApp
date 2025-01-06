using AutoMapper;
using BlogApp.Core.Entities;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Interfaces;
using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, BlogPostDto>
    {
        private readonly IRepository<BlogPost> _repository;
        private readonly IMapper _mapper;

        public GetBlogByIdQueryHandler(IRepository<BlogPost> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BlogPostDto> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the blog post by ID from the repository
            var blogPost = await _repository.GetByIdAsync(request.BlogId.ToString());

            if (blogPost == null)
            {
                throw new NotFoundException("Blog post not found.");
            }

            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);

            return blogPostDto;
        }
    }
}
