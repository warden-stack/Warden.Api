using System;
using System.Collections.Generic;
using System.Linq;

namespace Warden.Services.Features.Domain
{
    public class UserPaymentPlanMonthlySubscription
    {
        private HashSet<UserPaymentPlanFeatureUsage> _featureUsages =
            new HashSet<UserPaymentPlanFeatureUsage>();

        public DateTime From { get; protected set; }
        public DateTime To { get; protected set; }
        public decimal Price { get; protected set; }
        public bool IsFree => Price == 0;
        public DateTime? PaidAt { get; protected set; }
        public bool IsPaid => PaidAt.HasValue;

        public IEnumerable<UserPaymentPlanFeatureUsage> FeatureUsages
        {
            get { return _featureUsages; }
            protected set { _featureUsages = new HashSet<UserPaymentPlanFeatureUsage>(value); }
        }

        public UserPaymentPlanMonthlySubscription(DateTime from, IEnumerable<Feature> features)
        {
            From = from;
            To = from.AddMonths(1);
            FeatureUsages = features.Select(x => new UserPaymentPlanFeatureUsage(x));
        }

        public bool CanUseFeature(FeatureType feature)
        {
            var featureUsage = GetFeatureUsage(feature);

            return featureUsage.CanUse;
        }

        public void IncreaseFeatureUsage(FeatureType feature)
        {
            if (!CanUseFeature(feature))
                throw new InvalidOperationException($"Feature {feature} has reached its limit.");

            var featureUsage = GetFeatureUsage(feature);
            featureUsage.IncreaseUsage();
        }

        private UserPaymentPlanFeatureUsage GetFeatureUsage(FeatureType feature)
            => FeatureUsages.First(x => x.Feature == feature);

        public void MarkAsPaid()
        {
            if (IsPaid)
                return;

            PaidAt = DateTime.UtcNow;
        }
    }
}