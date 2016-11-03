using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Common.Domain;

namespace Warden.Services.Features.Domain
{
    public class UserPaymentPlan : IdentifiableEntity, ITimestampable
    {
        private HashSet<Feature> _features = new HashSet<Feature>();

        private HashSet<UserPaymentPlanMonthlySubscription> _monthlySubscriptions =
            new HashSet<UserPaymentPlanMonthlySubscription>();

        public string UserId { get; protected set; }
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
            UserId = user.UserId;
            PaymentPlanId = paymentPlan.Id;
            Name = paymentPlan.Name;
            MonthlyPrice = paymentPlan.MonthlyPrice;
            Available = paymentPlan.Available;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Features = paymentPlan.Features;
        }

        public void AddMonthlySubscription(DateTime from, IEnumerable<Feature> features)
        {
            var monthlySubscription = new UserPaymentPlanMonthlySubscription(from, features);
            _monthlySubscriptions.Add(monthlySubscription);
        }

        public UserPaymentPlanMonthlySubscription GetMonthlySubscription(DateTime date)
            => MonthlySubscriptions.FirstOrDefault(x => date.Date >= x.From.Date && date.Date <= x.To.Date);
    }
}