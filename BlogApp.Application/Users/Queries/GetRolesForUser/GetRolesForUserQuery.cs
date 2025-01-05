
using BlogApp.Dto.Role;
using MediatR;

namespace BlogApp.Application.Users.Queries.GetRolesForUser
{
    public class GetRolesForUserQuery : IRequest<List<RoleDto>>
    {
        public string UserId { get; set; }

        public GetRolesForUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
