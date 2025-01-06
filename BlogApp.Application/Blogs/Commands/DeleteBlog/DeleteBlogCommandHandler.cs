using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using MediatR;

namespace BlogApp.Application.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, bool>
    {
        private readonly IRepository<BlogPost> _repository;

        public DeleteBlogCommandHandler(IRepository<BlogPost> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the blog post by ID
            var blogPost = await _repository.GetByIdAsync(request.BlogId.ToString());

            if (blogPost == null)
            {
                // Blog post not found, return false
                return false;
            }

            // Delete the blog post from the repository
            var result = await _repository.DeleteAsync(blogPost);

            return result; 
        }
    }

}
