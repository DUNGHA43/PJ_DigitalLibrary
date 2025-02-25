using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class DocumentRepository : Repository<Documents>, IDocumentRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public DocumentRepository(DbDigitalLibraryContext context) : base(context) {
            _context = context;
        }

    }
}
