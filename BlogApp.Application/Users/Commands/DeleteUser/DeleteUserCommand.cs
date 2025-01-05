using MediatR;
using FluentResults;

namespace BlogApp.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }
}
