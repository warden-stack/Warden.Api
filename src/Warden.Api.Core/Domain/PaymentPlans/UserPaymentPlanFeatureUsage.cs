﻿using System;

namespace Warden.Api.Core.Domain.PaymentPlans
{
    public class UserPaymentPlanFeatureUsage
    {
        public FeatureType Feature { get; protected set; }
        public int Limit { get; protected set; }
        public int Usage { get; protected set; }
        public bool CanUse => Limit < Usage;

        public UserPaymentPlanFeatureUsage(Feature feature)
        {
            Feature = feature.Type;
            Limit = feature.Limit;
        }

        public void IncreaseUsage()
        {
            if(Usage == Limit)
                throw new InvalidOperationException($"Feature {Feature} has reached its limit.");

            Usage++;
        }
    }
}