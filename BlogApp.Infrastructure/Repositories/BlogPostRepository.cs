using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using BlogApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Repositories
{
    public class BlogPostRepository : IRepository<BlogPost>
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all blog posts
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        // Get a blog post by ID
        public async Task<BlogPost?> GetByIdAsync(string id)
        {
            return await _context.BlogPosts
                                 .FirstOrDefaultAsync(b => b.Id.ToString() == id);
        }

        // Add a new blog post
        public async Task AddAsync(BlogPost entity)
        {
            await _context.BlogPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Update an existing blog post
        public async Task UpdateAsync(BlogPost entity)
        {
            _context.BlogPosts.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Delete a blog post by ID
        public async Task DeleteAsync(string id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }
        }
    }

}
