using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class SubjectRepository : Repository<Subjects>, ISubjectRepository
    {
        public readonly DbDigitalLibraryContext _context;
        public SubjectRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteMultipleSubjectsAsync(List<int> subjectIds)
        {
            await DeleteMultipleAsync(subjectIds, "id");
        }

        public async Task<(IEnumerable<Subjects> Subjects, int TotalCount)> GetAllSubjectsAsync(int pageNumber, int pageSize, string searchName)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(c => c.subjectname!.Contains(searchName.Trim()));
            }

            var totalRecords = await query.CountAsync();

            var list = await query
                .OrderByDescending(c => c.createdate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (list, totalRecords);
        }

        public async Task<Subjects> GetSubjectByNameAsync(string name)
        {
            var subject = await _context.subjects.Where(s => s.subjectname!.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();
            
            if (subject == null) {
                return null;
            }

            return subject;
        }
    }
}
