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
                CONSTANT.Pro => true,
                CONSTANT.Vip => CONSTANT.Basic.Equals(requiredAccessLevel) || CONSTANT.Vip.Equals(requiredAccessLevel),
                CONSTANT.Basic => CONSTANT.Basic.Equals(requiredAccessLevel),
                _ => false
            };
        }

    }

    public static class CONSTANT
    {
        public const string Basic = "Basic";
        public const string Vip = "Vip";
        public const string Pro = "Pro";
    }
}
