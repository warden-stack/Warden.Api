using System;

namespace Warden.Api.Core.Domain.PaymentPlans
{
    public class UserPaymentPlanDailyFeatureUsage : UserPaymentPlanFeatureUsage
    {
        public DateTime Date { get; protected set; }

        public UserPaymentPlanDailyFeatureUsage(Feature feature, DateTime date) : base(feature)
        {
            Date = date;
        }
    }
}