
using Microsoft.AspNetCore.Http;

namespace BlogApp.Dto.Blog
{
    public class BlogPostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IFormFile? BannerImage { get; set; }
        public BlogStatus Status { get; set; } = BlogStatus.Draft;
    }
}
