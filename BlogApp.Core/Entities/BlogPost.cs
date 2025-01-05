namespace BlogApp.Core.Entities
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string BannerImagePath { get; set; } = string.Empty;
        public BlogStatus Status { get; set; } = BlogStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
    }
}
