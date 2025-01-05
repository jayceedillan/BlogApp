using BlogApp.Core.Entities;
using MediatR;

namespace BlogApp.Application.Users.Queries.GetUsers
{
    public class GetUserByIdQuery : IRequest<ApplicationUser>
    {
        public string UserId { get; set; }

        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
