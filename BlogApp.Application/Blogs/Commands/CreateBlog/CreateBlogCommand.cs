using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;
using MediatR;

namespace BlogApp.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<BlogPost>
    {
        public BlogPostDto blogPostDto { get; set; }
    }

}
