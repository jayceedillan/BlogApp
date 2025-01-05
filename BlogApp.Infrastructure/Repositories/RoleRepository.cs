using BlogApp.Core.Interfaces;
using BlogApp.Dto.Role;
using BlogApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlogApp.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RoleDto>> GetRolesForUserAsync(string userId)
        {
            // Assuming you have a relationship between Users and Roles, and a join table
            var roles = await _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Join(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
                .ToListAsync();

            var roleDtos = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name ?? string.Empty
            }).ToList();

            return roleDtos;
        }
    }
}
