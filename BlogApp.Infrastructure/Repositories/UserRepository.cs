using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using BlogApp.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddRolesToUserAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task RemoveRolesFromUserAsync(ApplicationUser user)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
        {
            await _context.Users.AddAsync(user); 
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser?> getUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
