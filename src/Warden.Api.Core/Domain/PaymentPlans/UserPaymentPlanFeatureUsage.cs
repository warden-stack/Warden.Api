namespace Warden.Api.Core.Domain.PaymentPlans
{
    public abstract class UserPaymentPlanFeatureUsage
    {
        public FeatureType Feature { get; protected set; }
        public int Limit { get; protected set; }
        public int Usage { get; protected set; }
        public bool CanUse => Limit < Usage;
    }
}