using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IUserSubscriptionsRepository : IRepository<UserSubcriptions>
    {
        Task<UserSubscriptionsDTO> FindUserSubscriptionsByUserId(int userId);
        Task<UserSubcriptions> FindUserSubscriptionsByPlanId(int planId);
    }
}
