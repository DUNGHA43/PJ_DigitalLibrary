using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class DocumentAuthorsRepository : Repository<DocumentAuthors>, IDocumentAuthorsRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public DocumentAuthorsRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentAuthors>> GetAllDocumentAuthorByDocumentId(int documentId)
        {
            var documentAuthors = await _dbSet.Where(x => x.documentid == documentId).ToListAsync();
            if (documentAuthors == null)
            {
                throw new ArgumentException($"DocumentAuthors with documentId {documentId} does not exits!");
            }
            return documentAuthors;
        }

        public async Task<DocumentAuthors> GetDocumentAuthorByDocumentAuthorId(int documentId, int authorId)
        {
            var documentAuthors = await _dbSet.Where(x => x.documentid == documentId && x.authorid == authorId).FirstOrDefaultAsync();
            return documentAuthors!;
        }
    }
}
