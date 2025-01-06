using BlogApp.Core.Entities;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByUserQuery : IRequest<IEnumerable<BlogPost>>
    {
        public string UserId { get; set; }

        public GetBlogsByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
