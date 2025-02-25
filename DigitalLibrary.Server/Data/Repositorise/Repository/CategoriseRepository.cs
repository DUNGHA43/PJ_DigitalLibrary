using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class CategoriseRepository : Repository<Categorise>, ICategoriseRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public CategoriseRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }


    }
}
