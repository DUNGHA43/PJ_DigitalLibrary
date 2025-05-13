using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class FavoListRepository : Repository<FavoList>, IFavoListReppository
    {
        private readonly DbDigitalLibraryContext _context;

        public FavoListRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FavoList> GetFavoListByDocumentAndUserId(int documentId, int userId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.documentid == documentId && x.userid == userId);
        }

        public async Task<IEnumerable<FavoListDTO>> ShowFavoListByUser(int userId)
        {
            var result = await _dbSet
            .Where(fl => fl.userid == userId)
            .Select(fl => new FavoListDTO
            {
                userid = fl.userid,
                documentid = fl.documentid,
                document = _context.documents
                    .Where(dm => dm.id == fl.documentid)
                    .Select(d => new DocumentsDTO
                    {
                        id = d.id,
                        title = d.title,
                        imageurl = d.imageurl,
                        accesslevel = d.accesslevel,
                        createdate = d.createdate,
                        description = d.description,
                        publisher = d.publisher,
                        Authors = _context.documentAuthors
                        .Where(da => da.documentid == d.id)
                        .Join(_context.authors, da => da.authorid, a => a.id, (da, a) => a.fullname)
                        .ToList(),
                        Categories = _context.documentCategorises
                        .Where(dc => dc.documentid == d.id)
                        .Join(_context.categories, dc => dc.categoryid, c => c.id, (dc, c) => c.catename)
                        .ToList(),
                        Subjects = _context.documentSubjects
                        .Where(ds => ds.documentid == d.id)
                        .Join(_context.subjects, ds => ds.subjectid, s => s.id, (ds, s) => s.subjectname)
                        .ToList()
                    }).FirstOrDefault()
            }).ToListAsync();

            if (result == null)
            {
                return new List<FavoListDTO>();
            }

            return result;
        }
    }
}
