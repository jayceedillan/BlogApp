
using BlogApp.Dto.User;
using FluentResults;
using MediatR;

namespace BlogApp.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result<string>>
    {
        public EditUserDto UserDto { get; set; }
    }

}
