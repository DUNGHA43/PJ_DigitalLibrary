using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentSubjectsServices
    {
        Task AddDocumentSubjectAsync(DocumentSubject documentSubject);
        Task DeleteDocumentSubject(int documentId, int subjectId);
        Task<IEnumerable<DocumentSubject>> GetAllDocumentSubjectsAsync(int documentId);
        Task<DocumentSubject> FindDocumentSubjectByDocumentSubjectIdAsync(int documentId, int subjectId);
    }
}
