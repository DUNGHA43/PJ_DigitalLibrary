using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUserSubscriptionServices
    {
        Task<UserSubscriptionsDTO> FindUserSubscriptionsByUserId(int userId);
        Task UpdateAsync(UserSubcriptions userSubcriptions);
    }
}
