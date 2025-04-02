using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Service
{
    public class UserSubscriptionServices : IUserSubscriptionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSubscriptionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserSubscriptionsDTO> FindUserSubscriptionsByUserId(int userId)
        {
            return await _unitOfWork.UserSubscriptions.FindUserSubscriptionsByUserId(userId);
        }
    }
}
