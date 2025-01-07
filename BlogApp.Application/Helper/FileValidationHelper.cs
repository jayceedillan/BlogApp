using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Helper
{
    public static class FileValidationHelper
    {
        public static bool IsValidBannerImage(string path)
        {
            // Check if the file extension is valid
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(path)?.ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return false;
            }

            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Length > 5 * 1024 * 1024) // Less than 5MB
                {
                    return false;
                }
            }

            return true;
        }
    }
}
