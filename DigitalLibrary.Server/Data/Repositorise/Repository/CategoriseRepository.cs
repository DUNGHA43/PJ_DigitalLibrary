using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class CategoriseRepository : Repository<Categories>, ICategoriesRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public CategoriseRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Categories> Categorise, int TotalCount)> GetAllCategoriesAsync(int pageNumber, int pageSize, string searchName)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(c => c.catename!.Contains(searchName.Trim()));
            }

            var totalRecords = await query.CountAsync();

            var list = await query
                .OrderByDescending(c => c.createdate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (list, totalRecords);
        }

        public async Task DeleteMultipleCategoriesAsync(List<int> categoryIds)
        {
            await DeleteMultipleAsync(categoryIds, "id");
        }
    }
}
