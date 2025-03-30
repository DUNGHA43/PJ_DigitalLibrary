using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class DocumentRepository : Repository<Documents>, IDocumentRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public DocumentRepository(DbDigitalLibraryContext context) : base(context) {
            _context = context;
        }

        public async Task DeleteMultipleDocumentsAsync(List<int> documentIds)
        {
            await DeleteMultipleAsync(documentIds, "id");
        }

        public async Task<IEnumerable<Documents>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<(IEnumerable<Documents> Documents, int TotalCount)> GetAllDocumentsAsync(int pageNumber, int pageSize, string searchName)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(c => c.title!.Contains(searchName.Trim()));
            }

            var totalRecords = await query.CountAsync();

            var list = await query
                .OrderByDescending(c => c.createdate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (list, totalRecords);
        }

        public async Task<IEnumerable<Documents>> GetDocumentHomePageAsync(int? subjectId = null, int? authorId = null, int? categoryId = null)
        {
            var query = _dbSet.Where(d => d.status == true);

            if (subjectId.HasValue)
            {
                query = query.Where(d => _context.documentSubjects
                    .Any(ds => ds.documentid == d.id && ds.subjectid == subjectId.Value));
            }

            if (authorId.HasValue)
            {
                query = query.Where(d => _context.documentAuthors
                    .Any(ds => ds.documentid == d.id && ds.authorid == authorId.Value));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(d => _context.documentCategorises
                    .Any(ds => ds.documentid == d.id && ds.categoryid == categoryId.Value));
            }

            var list = await query.OrderByDescending(d => d.createdate)
                .Select(d => new Documents
                {
                    id = d.id,
                    title = d.title,
                    imageurl = d.imageurl,
                    publisher = d.publisher,
                    status = d.status,
                    accesslevel = d.accesslevel
                })
                .ToListAsync();

            return list;
        }


        public async Task<List<Documents>> GetDocumentsByIdsAsync(List<int> documentIds)
        {
            return await _dbSet.Where(u => documentIds.Contains(u.id)).ToListAsync();
        }
    }
}
