using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IPaymentHistoryRepository : IRepository<PaymentHistory>
    {
        Task<IEnumerable<UserSubscriptionsDTO>> GetStatisticRevenueAsync();
        
    }
}
