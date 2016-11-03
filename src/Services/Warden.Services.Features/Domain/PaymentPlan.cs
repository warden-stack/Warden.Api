using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Common.Domain;
using Warden.Common.Extensions;

namespace Warden.Services.Features.Domain
{
    public class PaymentPlan : IdentifiableEntity, ITimestampable
    {
        private HashSet<Feature> _features = new HashSet<Feature>();
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

        protected PaymentPlan()
        {
        }

        public PaymentPlan(string name, decimal price, bool available = true)
        {
            SetName(name);
            SetMonthlyPrice(price);
            SetAvailability(available);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (name.Empty())
                throw new ArgumentException("Payment plan name can not be empty.");

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetMonthlyPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Payment plan monthly price can not be less than 0.");

            MonthlyPrice = price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAvailability(bool available)
        {
            if (Available == available)
                return;

            Available = available;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddFeatures(params Feature[] features)
        {
            foreach (var feature in features)
            {
                if (_features.Any(x => x.Type == feature.Type))
                {
                    throw new InvalidOperationException($"Feature {feature.Type} has been " +
                                                        $"already added to the payment plan: {Name}");

                }
                _features.Add(feature);
            }
            UpdatedAt = DateTime.UtcNow;
        }
    }
}