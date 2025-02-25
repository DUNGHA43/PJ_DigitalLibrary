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

        public async Task<Subjects> GetSubjectByNameAsync(string name)
        {
            var subject = await _context.subjects.Where(s => s.subjectname.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();
            
            if (subject == null) {
                return null;
            }

            return subject;
        }
    }
}
