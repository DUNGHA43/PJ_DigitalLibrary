using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class AuthorsRepository : Repository<Authors>, IAuthorsRepository
    {
        public readonly DbDigitalLibraryContext _context;

        public AuthorsRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }


    }
}
