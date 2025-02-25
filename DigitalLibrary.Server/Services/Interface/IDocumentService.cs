using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentService
    {
        Task<IEnumerable<Documents>> GetAllDocumentsAsync();
        Task<Documents> FindDocumentByIdAsync(int id);
        Task AddDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task UpdateDocumentAsync(Documents document);
        Task DeleteDocumentAsync(int id);
    }
}
