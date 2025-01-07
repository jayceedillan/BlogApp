using AutoMapper;
using BlogApp.Dto.User;
using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, CreateUserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
        CreateMap<ApplicationUser, EditUserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
        CreateMap<CreateUserDto, ApplicationUser>();
        CreateMap<EditUserDto, ApplicationUser>();
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());  
        CreateMap<BlogPostDto, BlogPost>().ReverseMap();
    }
}
