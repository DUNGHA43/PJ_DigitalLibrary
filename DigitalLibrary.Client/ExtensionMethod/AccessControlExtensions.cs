using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Client.ExtensionMethod
{
    public static class AccessControlExtensions
    {
        public static bool CanAccess(this UserSubscriptionsDTO subscription, string requiredAccessLevel)
        {
            if (subscription?.SubscriptionPlans == null) return false;

            return subscription.SubscriptionPlans.nameplan!.Trim() switch
            {
                "Pro" => true,
                "Vip" => requiredAccessLevel == "Basic" || requiredAccessLevel == "Vip",
                "Basic" => requiredAccessLevel == "Basic",
                _ => false
            };
        }
    }
}
