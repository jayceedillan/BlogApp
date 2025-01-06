
using Microsoft.AspNetCore.Http;

namespace BlogApp.Dto.Blog
{
    public class BlogPostDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? BannerImagePath { get; set; }
        public IFormFile? ImagePath { get; set; }  // Used for temporary file input (optional)
        public BlogStatus Status { get; set; } = BlogStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }

}
