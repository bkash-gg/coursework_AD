namespace AD_Coursework.Utils
{
    public static class ImageHelper
    {
        private static readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];

        public static async Task<string?> SaveImageAsync(IFormFile? imageFile, string subDirectory = "images")
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Invalid image format. Allowed formats: .jpg, .jpeg, .png, .gif");

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", subDirectory);
            var filePath = Path.Combine(folderPath, uniqueFileName);

            Directory.CreateDirectory(folderPath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/{subDirectory}/{uniqueFileName}";
        }
    }
}
