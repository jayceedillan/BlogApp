using BlogApp.Application.DTOs;
using BlogApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using BlogApp.Core.Entities;

namespace BlogApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<(bool Success, string Message)> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return (false, "User not found.");

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginDto.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
                return (true, "Login successful.");

            if (result.IsLockedOut)
                return (false, "Account is locked out.");

            return (false, "Invalid login attempt.");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
