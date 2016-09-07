using System;
using System.Collections.Generic;
using Warden.Api.Core.Domain.Users;

namespace Warden.Api.Core.Domain.PaymentPlans
{
    public class UserPaymentPlan : IdentifiableEntity, ITimestampable
    {
        private HashSet<Feature> _features = new HashSet<Feature>();

        private HashSet<UserPaymentPlanMonthlySubscription> _monthlySubscriptions =
            new HashSet<UserPaymentPlanMonthlySubscription>();

        public Guid UserId { get; protected set; }
        public Guid PaymentPlanId { get; protected set; }
        public string Name { get; protected set; }
        public decimal MonthlyPrice { get; protected set; }
        public bool Available { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public bool IsFree => MonthlyPrice == 0;

        public IEnumerable<Feature> Features
        {
            get { return _features; }
            protected set { _features = new HashSet<Feature>(value); }
        }

        public IEnumerable<UserPaymentPlanMonthlySubscription> MonthlySubscriptions
        {
            get { return _monthlySubscriptions; }
            protected set { _monthlySubscriptions = new HashSet<UserPaymentPlanMonthlySubscription>(value); }
        }

        protected UserPaymentPlan()
        {
        }

        public UserPaymentPlan(User user, PaymentPlan paymentPlan)
        {
            UserId = user.Id;
            PaymentPlanId = paymentPlan.Id;
            Name = paymentPlan.Name;
            MonthlyPrice = paymentPlan.MonthlyPrice;
            Available = paymentPlan.Available;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Features = paymentPlan.Features;
        }
    }
}