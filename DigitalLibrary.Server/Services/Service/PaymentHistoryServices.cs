using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class PaymentHistoryServices : IPaymentHistoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSubscriptionServices _userSubscriptionServices;

        public PaymentHistoryServices(IUnitOfWork unitOfWork, IUserSubscriptionServices userSubscriptionServices)
        {
            _unitOfWork = unitOfWork;
            _userSubscriptionServices = userSubscriptionServices;
        }

        public async Task AddPaymentHistoryAsync(int userId, int planId)
        {
            var subplan = await _unitOfWork.UserSubscriptions.FindUserSubscriptionsByUserId(userId);
            if (subplan == null)
            {
                throw new Exception("Subscription plan not found");
            }
            var paymentHistory = new PaymentHistory
            {
                createDate = DateTime.UtcNow,
                userId = userId,
                planId = planId
            };

            var subscription = new UserSubcriptions
            {
                id = subplan.id,
                userid = subplan.userid,
                planid = planId,
                redate = DateTime.Now,
                exdate = DateTime.Now.AddDays(30),
                status = true
            };

            await _userSubscriptionServices.UpdateAsync(subscription);
            await _unitOfWork.PaymentHistory.AddAsync(paymentHistory);
            await _unitOfWork.SaveChangeAsync();
        }

        public Task DeletePaymentHistoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentHistory> FindPaymentHistoryByIdAsync(int id)
        {
            return await _unitOfWork.PaymentHistory.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PaymentHistory>> GetAllPaymentHistorysAsync()
        {
            return await _unitOfWork.PaymentHistory.GetAllAsync();
        }

        public Task UpdatePaymentHistoryAsync(PaymentHistory paymentHistory)
        {
            throw new NotImplementedException();
        }
    }
}
