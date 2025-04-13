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

        public async Task<TrafficStatsDTO> GetStatsAsync(string period = "day")
        {
            var now = DateTime.UtcNow;
            var today = now.Date;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            var baseQuery = _context.trafficLogs.AsQueryable();

            var totalVisitsTask = await baseQuery.CountAsync();
            var todayVisitsTask = await baseQuery.CountAsync(x => x.createdate >= today);
            var monthVisitsTask = await baseQuery.CountAsync(x => x.createdate >= firstDayOfMonth);
            var dailyStatsTask = await GetDailyStatsAsync(baseQuery, today);
            var topPagesTask = await GetTopPagesAsync(baseQuery);

            return new TrafficStatsDTO
            {
                TotalVisits =  totalVisitsTask,
                TodayVisits =  todayVisitsTask,
                ThisMonthVisits =  monthVisitsTask,
                DailyStats =  dailyStatsTask,
                TopPages =  topPagesTask
            };
        }

        private async Task<List<DailyTraffic>> GetDailyStatsAsync(IQueryable<TrafficLog> baseQuery, DateTime today)
        {
            return await baseQuery
                .Where(x => x.createdate >= today.AddDays(-30))
                .GroupBy(x => x.createdate.Value)
                .Select(g => new DailyTraffic
                {
                    date = g.Key,
                    visit = g.Count()
                })
                .OrderBy(x => x.date)
                .ToListAsync();
        }

        private async Task<List<PageViewStats>> GetTopPagesAsync(IQueryable<TrafficLog> baseQuery, int topCount = 5)
        {
            return await baseQuery
                .GroupBy(x => x.url)
                .Select(g => new PageViewStats
                {
                    pageUrl = g.Key,
                    viewCount = g.Count()
                })
                .OrderByDescending(x => x.viewCount)
                .Take(topCount)
                .ToListAsync();
        }
    }
}
