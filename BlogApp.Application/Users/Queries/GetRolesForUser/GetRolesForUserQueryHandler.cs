
using BlogApp.Core.Interfaces;
using BlogApp.Dto.Role;
using MediatR;

namespace BlogApp.Application.Users.Queries.GetRolesForUser
{
    public class GetRolesForUserQueryHandler : IRequestHandler<GetRolesForUserQuery, List<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesForUserQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> Handle(GetRolesForUserQuery request, CancellationToken cancellationToken)
        {
            // Retrieve roles for the user from the repository
            var roles = await _roleRepository.GetRolesForUserAsync(request.UserId);

            // Return the roles as a list of RoleDto objects
            return roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList();
        }
    }
}
