using System;
using System.Collections.Generic;
using System.Linq;

namespace Warden.Api.Core.Domain.PaymentPlans
{
    public class UserPaymentPlanMonthlySubscription
    {
        public DateTime From { get; protected set; }
        public DateTime To { get; protected set; }
        public decimal Price { get; protected set; }
        public bool IsFree => Price == 0;
        public DateTime? PaidAt { get; protected set; }
        public bool IsPaid => PaidAt.HasValue;

        private HashSet<UserPaymentPlanMonthlyFeatureUsage> _monthlyFeatureUsages =
            new HashSet<UserPaymentPlanMonthlyFeatureUsage>();

        private HashSet<UserPaymentPlanDailyFeatureUsage> _dailyFeatureUsages =
            new HashSet<UserPaymentPlanDailyFeatureUsage>();

        public IEnumerable<UserPaymentPlanMonthlyFeatureUsage> MonthlyFeatureUsages
        {
            get { return _monthlyFeatureUsages; }
            protected set { _monthlyFeatureUsages = new HashSet<UserPaymentPlanMonthlyFeatureUsage>(value); }
        }

        public IEnumerable<UserPaymentPlanDailyFeatureUsage> DailyFeatureUsages
        {
            get { return _dailyFeatureUsages; }
            protected set { _dailyFeatureUsages = new HashSet<UserPaymentPlanDailyFeatureUsage>(value); }
        }

        public UserPaymentPlanMonthlySubscription(DateTime from, IEnumerable<Feature> features)
        {
            From = from;
            To = from.AddMonths(1);

            var monthlyFeatures = features.Where(x => !x.IsDailyLimit);
            var dailyFeatures = features.Where(x => x.IsDailyLimit).ToList();
            MonthlyFeatureUsages = monthlyFeatures.Select(x => new UserPaymentPlanMonthlyFeatureUsage(x));

            for (var day = From.Date; day.Date <= To.Date; day = day.AddDays(1))
            {
                foreach (var dailyFeature in dailyFeatures)
                {
                    _dailyFeatureUsages.Add(new UserPaymentPlanDailyFeatureUsage(dailyFeature, day));
                }
            }
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
        {
            if (feature == FeatureType.WardenChecksPerDay)
                return DailyFeatureUsages.First(x => x.Feature == feature);

            return MonthlyFeatureUsages.First(x => x.Feature == feature);
        }

        public void MarkAsPaid()
        {
            if (IsPaid)
                return;

            PaidAt = DateTime.UtcNow;
        }
    }
}