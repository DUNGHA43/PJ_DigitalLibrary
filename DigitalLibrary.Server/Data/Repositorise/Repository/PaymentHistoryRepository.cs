using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class PaymentHistoryRepository : Repository<PaymentHistory>, IPaymentHistoryRepository
    {
        private readonly DbDigitalLibraryContext _context;
        public PaymentHistoryRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<UserSubscriptionsDTO>> GetStatisticRevenueAsync()
        {
            throw new NotImplementedException();
        }
    }
}
