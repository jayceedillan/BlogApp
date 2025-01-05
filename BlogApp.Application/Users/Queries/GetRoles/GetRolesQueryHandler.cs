using BlogApp.Dto.Role;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Users.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolesQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => new RoleDto { Id = r.Id, Name = string.IsNullOrEmpty(r.Name) ? "" : r.Name }).ToList();
        }
    }
}
