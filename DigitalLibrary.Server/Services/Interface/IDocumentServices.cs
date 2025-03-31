using DigitalLibrary.Server.Model;
using Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentServices
    {
        Task<IEnumerable<Documents>> GetAllDocumentsAsync();
        Task<Documents> FindDocumentByIdAsync(int id);
        Task AddDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task UpdateDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task DeleteDocumentAsync(int id);
        Task<(IEnumerable<Documents> Documents, int TotalCount)> GetAllDocumentsAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleDocumentsAsync(List<int> documetnsIds);
        Task<List<Documents>> GetDocumentsByIdsAsync(List<int> documentIds);
        Task<IEnumerable<Documents>> GetDocumentHomePageAsync(int? subjectId = null, int? authorId = null, int? categoryId = null, string? accesslevel = null, string? searchName = null);
    }
}
