namespace GMS.API.Helpers
{
    public static class FileHelpers
    {
        public static async Task<string> SaveFileAsync(IFormFile file)
        {
            var relativePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var physicalPath = GetPhysicalPath(relativePath);

            using var fs = File.Create(physicalPath);
            await file.CopyToAsync(fs);

            return relativePath;
        }

        public static Task DeleteFileAsync(string relativePath)
        {
            File.Delete(GetPhysicalPath(relativePath));

            return Task.CompletedTask;
        }

        private static string GetPhysicalPath(string relativePath)
            => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
    }
}
