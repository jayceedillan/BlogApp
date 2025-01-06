using Microsoft.AspNetCore.Http;

namespace BlogApp.Application.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderPath);
        bool DeleteFile(string filePath);
        bool ValidateFile(IFormFile file);
    }

}
