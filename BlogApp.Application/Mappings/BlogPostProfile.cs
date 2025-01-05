using AutoMapper;
using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;

namespace BlogApp.Application.Mappings
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPostDto, BlogPost>()
                .ForMember(dest => dest.BannerImagePath, opt => opt.Ignore());
        }
    }
}
