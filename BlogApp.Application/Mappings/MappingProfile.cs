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
    //         .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id mapping
    //.AfterMap((src, dest) => dest.Id = null); // Set Id to null after mapping
        CreateMap<EditUserDto, ApplicationUser>();
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());  
        CreateMap<BlogPostDto, BlogPost>().ReverseMap();
    }
}
