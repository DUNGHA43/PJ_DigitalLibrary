using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IPaymentHistoryRepository : IRepository<PaymentHistory>
    {
        Task<IEnumerable<PlanRevenueDTO>> GetStatisticRevenueAsync(int? day = null, int? month = null, int? year = null);
        Task<IEnumerable<MonthlyPlanRevenueDTO>> GetMonthlyRevenueByYearAsync(int? year);


    }
}
