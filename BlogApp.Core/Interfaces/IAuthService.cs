using BlogApp.Application.DTOs;

namespace BlogApp.Core.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
