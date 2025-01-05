
using BlogApp.Dto.User;
using FluentResults;
using MediatR;

namespace BlogApp.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public CreateUserDto UserDto { get; set; }
    }
}
