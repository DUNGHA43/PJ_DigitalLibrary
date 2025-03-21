using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IDocumentSubjectsRepository : IRepository<DocumentSubject>
    {
        Task<IEnumerable<DocumentSubject>> GetAllDocumentSubjectByDocumentId(int documentId);
        Task<DocumentSubject> GetDocumentSubjectByDocumentSubjectId(int documentId, int subjectId);
    }
}
