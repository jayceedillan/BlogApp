using AutoMapper;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByStatusQueryHandler : IRequestHandler<GetBlogsByStatusQuery, IEnumerable<BlogPostDto>>
    {
        private readonly IRepository<BlogPost> _repository;
        private readonly IMapper _mapper;

        public GetBlogsByStatusQueryHandler(IRepository<BlogPost> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPostDto>> Handle(GetBlogsByStatusQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _repository.GetAllAsync();
            var filteredBlogs = blogs.Where(b => b.Status == request.Status).ToList();
            return _mapper.Map<IEnumerable<BlogPostDto>>(filteredBlogs);
        }

    }

}
