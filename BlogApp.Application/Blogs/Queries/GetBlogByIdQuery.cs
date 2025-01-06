using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Queries
{
    public class GetBlogByIdQuery : IRequest<BlogPostDto>
    {
        public Guid BlogId { get; set; }

        public GetBlogByIdQuery(Guid blogId)
        {
            BlogId = blogId;
        }
    }
}
