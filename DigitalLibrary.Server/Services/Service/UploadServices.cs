using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class UploadServices : IUploadServices
    {
        private readonly IWebHostEnvironment _env;

        public UploadServices(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path dose not valid!");
            }

            string fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/').Replace("/", "\\"));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<string> FindFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("File path dose not valid!");
            }

            string fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/').Replace("/", "\\"));

            if (File.Exists(fullPath))
            {
                return fullPath;
            }
            return string.Empty;
        }

        public async Task<FileUpload> UploadFileDataAsync(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                throw new ArgumentException("Please select a valid file!");
            }

            string uploadFolder = Path.Combine(_env.WebRootPath, "UploadDocuments");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            string fileUrl = $"/UploadDocuments/{uniqueFileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUpload = new FileUpload()
            {
                filePath = filePath,
                fileUrl = fileUrl
            };

            return fileUpload;
        }

        public async Task<FileUpload> UploadFileImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Please select a valid file!");
            }

            string uploadFolder = Path.Combine(_env.WebRootPath, "UploadImages");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            string fileUrl = $"/UploadImages/{uniqueFileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUpload = new FileUpload()
            {
                filePath = filePath,
                fileUrl = fileUrl
            };

            return fileUpload;
        }
    }
}
