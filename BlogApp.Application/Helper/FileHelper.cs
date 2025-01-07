using Microsoft.AspNetCore.Http;

namespace BlogApp.Application.Helper
{
    public static class FileHelper
    {
        public static IFormFile ConvertToIFormFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found at the specified path.", filePath);
            }

            var fileBytes = File.ReadAllBytes(filePath);
            var stream = new MemoryStream(fileBytes);

            var formFile = new FormFile(stream, 0, fileBytes.Length, "file", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(filePath), 
            };

            return formFile;
        }

        private static string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    return "application/octet-stream"; 
            }
        }
    }
}
