using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IDocumentRepository : IRepository<Documents>
    {
        Task<(IEnumerable<Documents> Documents, int TotalCount)> GetAllDocumentsAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleDocumentsAsync(List<int> documentIds);
        Task<List<Documents>> GetDocumentsByIdsAsync(List<int> documentIds);
    }
}
