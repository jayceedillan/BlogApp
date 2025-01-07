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

        public GetBlogsByUserQueryHandler(IRepository<BlogPost> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPostDto>> Handle(GetBlogsByUserQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all blog posts
            var blogs = await _repository.GetAllAsync();

            // Filter blogs by the UserId
            var filteredBlogs = blogs.Where(b => b.AuthorId == request.UserId);

            var blogPostDtos = _mapper.Map<IEnumerable<BlogPostDto>>(filteredBlogs);

            return blogPostDtos;
        }
    }

}
