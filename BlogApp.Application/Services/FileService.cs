using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Application.Services
{
   using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class FileService : IFileService
{
    private readonly string _baseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

    public FileService()
    {
        if (!Directory.Exists(_baseFilePath))
        {
            Directory.CreateDirectory(_baseFilePath);
        }
    }

    // Method to save the file
    public async Task<string> SaveFileAsync(IFormFile file, string folderPath)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }

        // Validate file before saving
        if (!ValidateFile(file))
        {
            throw new ArgumentException("Invalid file type or size.");
        }

        // Combine folder path with file name to create the full path
        string folderDirectory = Path.Combine(_baseFilePath, folderPath);
        if (!Directory.Exists(folderDirectory))
        {
            Directory.CreateDirectory(folderDirectory);
        }

        // Create a unique file name to avoid conflicts
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(folderDirectory, fileName);

        // Save the file to the disk
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return the relative path or URL for the file
        return $"/uploads/{folderPath}/{fileName}";
    }

    // Method to delete the file
    public bool DeleteFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return false;
        }

        // Construct the full path to the file
        string fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

        if (File.Exists(fullFilePath))
        {
            File.Delete(fullFilePath);
            return true;
        }

        return false;
    }

        // Method to validate the file
        public bool ValidateFile(IFormFile file)
        {
            // File type validation (only allow specific types like JPG, PNG, GIF)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            // File size validation (less than 5MB)
            if (file.Length > 5 * 1024 * 1024) // 5 MB
            {
                return false;
            }

            return true;
        }

    }
}
