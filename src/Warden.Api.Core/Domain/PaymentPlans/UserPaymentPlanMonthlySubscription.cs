using System;
using System.Collections.Generic;

namespace Warden.Api.Core.Domain.PaymentPlans
{
    public class UserPaymentPlanMonthlySubscription
    {
        public DateTime From { get; protected set; }
        public DateTime To { get; protected set; }
        public decimal Price { get; protected set; }
        public bool IsPaid { get; protected set; }
        public bool IsFree => Price == 0;
        public DateTime PaidAt { get; protected set; }

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
    }
}