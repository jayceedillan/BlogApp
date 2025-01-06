using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByStatusQueryHandler : IRequestHandler<GetBlogsByStatusQuery, IEnumerable<BlogPost>>
    {
        private readonly IRepository<BlogPost> _repository;

        public GetBlogsByStatusQueryHandler(IRepository<BlogPost> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlogPost>> Handle(GetBlogsByStatusQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _repository.GetAllAsync();
            return blogs.Where(b => b.Status == request.Status).ToList();
        }

    }

}
