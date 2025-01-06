using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommand : IRequest<BlogPostDto>
    {
        public BlogPostDto blogPostDto { get; set; }
    }
}
