using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentAuthorServices
    {
        Task AddDocumentAuthorAsync(DocumentAuthors documentAuthor);
        Task DeleteDocumentAuthor(int documentId, int authorId);
        Task<IEnumerable<DocumentAuthors>> GetAllDocumentAuthorsAsync(int documentId);
        Task<DocumentAuthors> FindDocumentAuthorByDocumentAuthorIdAsync(int documentId, int authorId);
    }
}
