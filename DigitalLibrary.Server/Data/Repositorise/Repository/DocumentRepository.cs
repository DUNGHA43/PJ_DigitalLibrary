using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class DocumentRepository : Repository<Documents>, IDocumentRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public DocumentRepository(DbDigitalLibraryContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Documents>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
