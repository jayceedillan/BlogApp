using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogsByUserQuery : IRequest<IEnumerable<BlogPostDto>>
    {
        public string UserId { get; set; }

        public GetBlogsByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
