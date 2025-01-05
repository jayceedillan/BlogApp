using BlogApp.Dto.Role;
using MediatR;

namespace BlogApp.Application.Users.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
    }
}
