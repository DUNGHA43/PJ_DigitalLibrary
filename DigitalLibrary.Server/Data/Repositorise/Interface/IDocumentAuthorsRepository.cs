using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IDocumentAuthorsRepository : IRepository<DocumentAuthors>
    {
        Task<IEnumerable<DocumentAuthors>> GetAllDocumentAuthorByDocumentId(int documentId);
        Task<DocumentAuthors> GetDocumentAuthorByDocumentAuthorId(int documentId, int authorId);
    }
}
