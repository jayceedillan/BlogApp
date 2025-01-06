using MediatR;

namespace BlogApp.Application.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommand : IRequest<bool> 
    {
        public string BlogId { get; set; }

        public DeleteBlogCommand(string blogId)
        {
            BlogId = blogId;
        }
    }
}
