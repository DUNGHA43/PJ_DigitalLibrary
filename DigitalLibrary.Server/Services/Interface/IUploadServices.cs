using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUploadServices
    {
        Task<FileUpload> UploadFileDataAsync(IFormFile file);
        Task<FileUpload> UploadFileImageAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string filePath);
        Task<string> FindFile(string filePath);
    }
}
