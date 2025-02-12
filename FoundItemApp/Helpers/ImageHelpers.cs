namespace FoundItemApp.Helpers
{
    public static class ImageHelpers
    {
        public static async Task<List<string>?> UploadImages(List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                return null;
            }

            string[] extensions = { "jpg", "jpeg", "png" };

            var imageAddressList = new List<string>();

            foreach (var image in images)
            {
                string imageExtension = Path.GetExtension(image.FileName);

                if (!extensions.Contains(imageExtension))
                {
                    throw new InvalidOperationException("Wrong format of the image, needs to be a jpg, jpeg or png");
                }

                if (image.Length > 5 * 1024 * 1024)
                {
                    throw new InvalidOperationException("The image is too large, cannot exceed 5mb");
                }

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", image.FileName);

                try
                {
                    await using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

                imageAddressList.Add(filePath);
            }

            return imageAddressList;
        }

        public static async Task<FileStream?> GetImage(string imagePath)
        {
            if (!System.IO.File.Exists(imagePath) || imagePath == null)
            {
                return null;
            }

            var fileStream = new FileStream(
                imagePath,
                FileMode.Open,
                FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);

            return fileStream;
        }
    }
}
