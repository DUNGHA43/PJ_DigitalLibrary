using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUploadService
    {
        Task<FileUpload> UploadFileDataAsync(IFormFile file);
        Task<FileUpload> UploadFileImageAsync(IFormFile file);
        Task DeleteFileAsync(string filePath);
    }
}
