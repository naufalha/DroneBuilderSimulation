using DroneBuildSimulation.Constants;
using DroneBuildSimulation.Services.Interfaces;

namespace DroneBuildSimulation.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null) return null;

            // Validasi Ekstensi
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(ext))
            {
                throw new ArgumentException($"Invalid file type. Allowed: {string.Join(", ", _allowedExtensions)}");
            }

            // Generate nama file unik
            string uniqueFileName = $"{Guid.NewGuid()}{ext}";
            
            // Tentukan path folder
            string uploadsFolder = Path.Combine(_env.WebRootPath, folderName);
            
            // Buat folder jika belum ada
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Simpan file
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        public void DeleteFile(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            string filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}