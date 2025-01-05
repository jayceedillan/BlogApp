using AutoMapper;
using BlogApp.Core.Entities;
using BlogApp.Dto.User;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Users.Queries.GetUsers
{

    public class GetUsersQuery : IRequest<Result<List<UserDto>>> { }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<List<UserDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
       
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserDto>(user);

                userDto.Roles = roles.ToList();
                userDtos.Add(userDto);
               
            }

            return Result.Ok(userDtos);
        }

    }
}
