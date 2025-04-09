using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IPaymentHistoryServices
    {
        Task<PaymentHistory> FindPaymentHistoryByIdAsync(int id);
        Task AddPaymentHistoryAsync(int userId, int planId);
        Task UpdatePaymentHistoryAsync(PaymentHistory paymentHistory);
        Task DeletePaymentHistoryAsync(int id);
        Task<IEnumerable<PaymentHistory>> GetAllPaymentHistorysAsync();
    }
}
