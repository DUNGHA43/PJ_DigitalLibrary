using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class StatisticRepository : Repository<Statistics>, IStatisticRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public StatisticRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ViewAndDowloadStatisticResponse> GetViewAndDowloadStatisticAsync()
        {
            var response = new ViewAndDowloadStatisticResponse();

            var allStatistics = await _context.statistics
                .Include(s => s.Document)
                .ToListAsync();

            var groupedStatistics = allStatistics
                .GroupBy(s => s.Document.id)
                .Select(rs => new StatisticDTO
                {
                    documentid = rs.Key,
                    Document = new DocumentsDTO
                    {
                        id = rs.First().Document.id,
                        title = rs.First().Document.title,
                        description = rs.First().Document.description,
                        createdate = rs.First().Document.createdate,
                        publisher = rs.First().Document.publisher,
                    },
                    views = rs.Sum(s => s.views),
                    dowloaded = rs.Sum(s => s.dowloaded)
                }).ToList();

            response.FullData = groupedStatistics
                .OrderByDescending(s => s.views)
                .ToList();

            response.ChartData = groupedStatistics
                    .OrderByDescending(s => s.views)
                    .Take(10)
                    .Select(s => new ChartDataStatisticDocumentDTO
                    {
                        DocumentName = s.Document.title,
                        Views = s.views,
                        Downloads = s.dowloaded
                    }).ToList();

            response.TopViewsDocuments = groupedStatistics
                .OrderByDescending(s => s.views)
                .Take(5)
                .ToList();

            response.TopDowloadedDocuments = groupedStatistics
                .OrderByDescending(s => s.dowloaded)
                .Take(5)
                .ToList();

            response.TotalViews = allStatistics.Sum(s => s.views) ?? 0;
            response.TotalDownloads = allStatistics.Sum(s => s.dowloaded) ?? 0;

            return response;
        } 
    }
}
