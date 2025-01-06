using AutoMapper;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByUserQueryHandler : IRequestHandler<GetBlogsByUserQuery, IEnumerable<BlogPostDto>>
    {
        private readonly IRepository<BlogPost> _repository;
        private readonly IMapper _mapper;

        // Constructor to inject the repository and AutoMapper
        public GetBlogsByUserQueryHandler(IRepository<BlogPost> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Handle method to return filtered blogs for the user
        public async Task<IEnumerable<BlogPostDto>> Handle(GetBlogsByUserQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all blog posts
            var blogs = await _repository.GetAllAsync();

            // Filter blogs by the UserId
            var filteredBlogs = blogs.Where(b => b.AuthorId == request.UserId);

            // Map the filtered blogs to BlogPostDto
            var blogPostDtos = _mapper.Map<IEnumerable<BlogPostDto>>(filteredBlogs);

            return blogPostDtos;
        }
    }

}
