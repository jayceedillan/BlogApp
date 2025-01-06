
using Microsoft.AspNetCore.Http;

namespace BlogApp.Dto.Blog
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IFormFile BannerImagePath { get; set; }
        public BlogStatus Status { get; set; } = BlogStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
        public string AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}
