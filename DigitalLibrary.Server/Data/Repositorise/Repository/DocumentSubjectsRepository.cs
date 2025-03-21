using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class DocumentSubjectsRepository : Repository<DocumentSubject>, IDocumentSubjectsRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public DocumentSubjectsRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentSubject>> GetAllDocumentSubjectByDocumentId(int documentId)
        {
            var documentSubject = await _dbSet.Where(x => x.documentid == documentId).ToListAsync();
            if (documentSubject == null)
            {
                throw new ArgumentException($"DocumentSubjects with documentId {documentId} does not exits!");
            }
            return documentSubject;
        }

        public async Task<DocumentSubject> GetDocumentSubjectByDocumentSubjectId(int documentId, int subjectId)
        {
            var documentSubject = await _dbSet.Where(x => x.documentid == documentId && x.subjectid == subjectId).FirstOrDefaultAsync();
            return documentSubject!;
        }
    }
}
