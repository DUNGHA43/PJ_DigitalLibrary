using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentService
    {
        Task<IEnumerable<Documents>> GetAllDocumentsAsync();
        Task<Documents> FindDocumentByIdAsync(int id);
        Task AddDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task UpdateDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task DeleteDocumentAsync(int id);
        Task<(IEnumerable<Documents> Documents, int TotalCount)> GetAllDocumentsAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleDocumentsAsync(List<int> documetnsIds);
    }
}
