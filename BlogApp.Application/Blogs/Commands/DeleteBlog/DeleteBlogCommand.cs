using MediatR;

namespace BlogApp.Application.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommand : IRequest<bool> 
    {
        public Guid BlogId { get; set; }

        public DeleteBlogCommand(Guid blogId)
        {
            BlogId = blogId;
        }
    }
}
