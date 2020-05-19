using System;
using System.IO;

namespace MealTrackWebAPI.Helpers
{
    public static class ImageHelper
    {
        public static string ConvertImageToBase64String(string imagePath)
        {
            string fullPath = Path.Combine("Content/images/",imagePath);
            if(!File.Exists(fullPath))
                return string.Empty;
            byte[] imageBytes = File.ReadAllBytes(fullPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
