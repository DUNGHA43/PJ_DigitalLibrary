using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class PaymentHistoryRepository : Repository<PaymentHistory>, IPaymentHistoryRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public PaymentHistoryRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanRevenueDTO>> GetStatisticRevenueAsync(int? day = null, int? month = null, int? year = null)
        {
            var query = from PaymentHistory in _context.paymentHistories
                        join Plan in _context.subscriptionPlans on PaymentHistory.planId equals Plan.id
                        select new
                        {
                            PaymentHistory,
                            Plan
                        };

            if (day.HasValue)
                query = query.Where(x => x.PaymentHistory.createDate.Day == day.Value);
            if (month.HasValue)
                query = query.Where(x => x.PaymentHistory.createDate.Month == month.Value);
            if (year.HasValue)
                query = query.Where(x => x.PaymentHistory.createDate.Year == year.Value);

            var result = await query
                .GroupBy(x => new { x.Plan.id, x.Plan.nameplan })
                .Select(g => new PlanRevenueDTO
                {
                    PlanName = g.Key.nameplan.Trim(),
                    TotalTransactions = g.Count(),
                    TotalRevenue = g.Sum(x => x.Plan.price)
                })
                .OrderByDescending(x => x.TotalRevenue).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<MonthlyPlanRevenueDTO>> GetMonthlyRevenueByYearAsync(int? year)
        {
            int currentYear = DateTime.UtcNow.Year;
            year ??= currentYear;

            var query = from history in _context.paymentHistories
                        join plan in _context.subscriptionPlans on history.planId equals plan.id
                        where history.createDate.Year == year
                        group new { history, plan } by new { history.createDate.Month, plan.nameplan } into g
                        select new MonthlyPlanRevenueDTO
                        {
                            Month = g.Key.Month,
                            PlanName = g.Key.nameplan.Trim(),
                            TotalTransactions = g.Count(),
                            TotalRevenue = g.Sum(x => x.plan.price)
                        };

            return await query.OrderBy(x => x.Month).ToListAsync();
        }

    }
}
