using Microsoft.AspNetCore.Http;

namespace BlogApp.Application.Helper
{
    public static class FileHelper
    {
        public static IFormFile ConvertToIFormFile(string filePath)
        {
            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found at the specified path.", filePath);
            }

            // Read the file into a stream
            var fileBytes = File.ReadAllBytes(filePath);
            var stream = new MemoryStream(fileBytes);

            // Create an IFormFile using the MemoryStream
            var formFile = new FormFile(stream, 0, fileBytes.Length, "file", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(filePath), // Set the appropriate content type
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
                    return "application/octet-stream"; // Default MIME type
            }
        }
    }
}
