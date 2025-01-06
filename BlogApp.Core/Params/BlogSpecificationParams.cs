using BlogApp.Dto.Blog;

namespace BlogApp.Core.Params
{
    public class BlogSpecificationParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string CurrentUserId { get; set; }  
        public BlogStatus? Status { get; set; }
    }
}
