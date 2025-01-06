using AutoMapper;
using BlogApp.Dto.User;
using BlogApp.Core.Entities;
using BlogApp.Dto.Blog;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping ApplicationUser to CreateUserDto and EditUserDto
        CreateMap<ApplicationUser, CreateUserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
        CreateMap<ApplicationUser, EditUserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

        // Mapping CreateUserDto and EditUserDto to ApplicationUser
        CreateMap<CreateUserDto, ApplicationUser>();
        CreateMap<EditUserDto, ApplicationUser>();

        // Add mapping for ApplicationUser to UserDto
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());  // You can choose to map roles if needed
        CreateMap<BlogPostDto, BlogPost>().ReverseMap();
    }
}
