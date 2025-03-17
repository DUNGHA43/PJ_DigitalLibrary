using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class AuthorsRepository : Repository<Authors>, IAuthorsRepository
    {
        public readonly DbDigitalLibraryContext _context;

        public AuthorsRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Authors> Authors, int TotalCount)> GetAllAuthorsAsync(int pageNumber, int pageSize, string searchName, string searchNation)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(a => a.fullname!.Contains(searchName.Trim()));
            }

            if (!string.IsNullOrEmpty(searchNation))
            {
                query = query.Where(a => a.nation.nationname!.Contains(searchNation.Trim()));
            }

            var totalRecords = await query.CountAsync();

            var list = await query
                .OrderByDescending(a => a.createdate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (list, totalRecords);
        }

        public async Task DeleteMultipleAuthorsAsync(List<int> authorIds)
        {
            await DeleteMultipleAsync(authorIds, "id");
        }
    }
}
