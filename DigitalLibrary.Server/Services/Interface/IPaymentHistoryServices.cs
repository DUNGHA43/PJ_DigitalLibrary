using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IPaymentHistoryServices
    {
        Task<PaymentHistory> FindPaymentHistoryByIdAsync(int id);
        Task AddPaymentHistoryAsync(int userId, int planId);
        Task UpdatePaymentHistoryAsync(PaymentHistory paymentHistory);
        Task DeletePaymentHistoryAsync(int id);
        Task<IEnumerable<PaymentHistory>> GetAllPaymentHistorysAsync();
        Task<IEnumerable<PlanRevenueDTO>> GetStatisticRevenueAsync(int? day = null, int? month = null, int? year = null);
        Task<IEnumerable<MonthlyPlanRevenueDTO>> GetMonthlyRevenueByYearAsync(int? year);
    }
}
