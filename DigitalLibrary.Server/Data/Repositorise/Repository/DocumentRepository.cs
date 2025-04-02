using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IEnumerable<Documents>> GetDocumentHomePageAsync(int? subjectId = null, int? authorId = null, int? categoryId = null, string? accesslevel = null, string? searchName = null)
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

            if (!string.IsNullOrEmpty(accesslevel))
            {
                query = query.Where(d => d.accesslevel!.Contains(accesslevel));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(d => d.title!.Contains(searchName));
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

        public async Task<IEnumerable<DocumentGroupDTO>> GetDocumentByFilterAsync(int? subjectId = null, int? authorId = null, int? categoryId = null,
        string? accesslevel = null, string? searchName = null, string? filterGroup = null, int page = 1, int pageSize = 10)
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

            if (!string.IsNullOrEmpty(accesslevel))
            {
                query = query.Where(d => d.accesslevel!.Contains(accesslevel));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(d => d.title!.Contains(searchName));
            }

            int totalCount = await query.CountAsync();

            var documents = await query
                .OrderByDescending(d => d.createdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DocumentsDTO
                {
                    id = d.id,
                    title = d.title,
                    imageurl = d.imageurl,
                    accesslevel = d.accesslevel,
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
                })
                .ToListAsync();

            // Nếu không có filterGroup, trả về danh sách không nhóm
            if (string.IsNullOrEmpty(filterGroup))
            {
                return new List<DocumentGroupDTO>
                {
                    new DocumentGroupDTO { GroupKey = null, Documents = documents, TotalCount = totalCount }
                };
            }

            // Nhóm dữ liệu theo filterGroup
            return filterGroup.ToLower() switch
            {
                "author" => documents
                            .SelectMany(d => d.Authors.Select(a => new { AuthorName = a, Document = d }))
                            .GroupBy(d => d.AuthorName)
                            .Select(g => new DocumentGroupDTO
                            {
                                GroupKey = g.Key,
                                Documents = g.Select(x => x.Document).Distinct().ToList(),
                                TotalCount = totalCount
                            })
                            .ToList(),

                "category" => documents
                              .SelectMany(d => d.Categories.Select(c => new { CategoryName = c, Document = d }))
                              .GroupBy(d => d.CategoryName)
                              .Select(g => new DocumentGroupDTO
                              {
                                  GroupKey = g.Key,
                                  Documents = g.Select(x => x.Document).Distinct().ToList(),
                                  TotalCount = totalCount
                              }),

                "subject" => documents
                .SelectMany(d => d.Subjects.Select(s => new { SubjectName = s, Document = d }))
                                .GroupBy(d => d.SubjectName)
                                .Select(g => new DocumentGroupDTO
                                {
                                    GroupKey = g.Key,
                                    Documents = g.Select(x => x.Document).Distinct().ToList(),
                                    TotalCount = totalCount
                                }),

                "accesslevel" => documents.GroupBy(d => d.accesslevel)
                                          .Select(g => new DocumentGroupDTO
                                          {
                                              GroupKey = g.Key,
                                              Documents = g.ToList(),
                                              TotalCount = totalCount
                                          })
                                          .ToList(),

                _ => new List<DocumentGroupDTO>
                {
                    new DocumentGroupDTO { GroupKey = "Unknown", Documents = documents, TotalCount = totalCount }
                }
            };
        }


        public async Task<List<Documents>> GetDocumentsByIdsAsync(List<int> documentIds)
        {
            return await _dbSet.Where(u => documentIds.Contains(u.id)).ToListAsync();
        }

        public async Task<DocumentsDTO> FindDocumentDetailByIdAsync(int id)
        {
            var query = _dbSet.Where(d => d.id == id).AsQueryable();

            var documents = await query
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
                })
                .FirstOrDefaultAsync();

            return documents!;
        }
    }
}
