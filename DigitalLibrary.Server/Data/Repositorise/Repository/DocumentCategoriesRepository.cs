using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class DocumentCategoriesRepository : Repository<DocumentCategories>, IDocumentCategoriesRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public DocumentCategoriesRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentCategories>> GetAllDocumentCategoriesByDocumentId(int documentId)
        {
            var documentCategory = await _dbSet.Where(x => x.documentid == documentId).ToListAsync();
            if (documentCategory == null)
            {
                throw new ArgumentException($"DocumentCategories with documentId {documentId} does not exits!");
            }
            return documentCategory;
        }

        public async Task<DocumentCategories> GetDocumentCategoriesByDocumentCategoriesId(int documentId, int cateId)
        {
            var documentCategory = await _dbSet.Where(x => x.documentid == documentId && x.categoryid == cateId).FirstOrDefaultAsync();

            return documentCategory!;
        }
    }
}
