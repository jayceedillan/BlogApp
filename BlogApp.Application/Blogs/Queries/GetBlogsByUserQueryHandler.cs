using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByUserIdQueryHandler : IRequestHandler<GetBlogsByUserQuery, IEnumerable<BlogPost>>
    {
        private readonly IRepository<BlogPost> _repository;

        public GetBlogsByUserIdQueryHandler(IRepository<BlogPost> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlogPost>> Handle(GetBlogsByUserQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _repository.GetAllAsync();
            return blogs.Where(b => b.AuthorId == request.UserId).ToList();
        }
    }
}
