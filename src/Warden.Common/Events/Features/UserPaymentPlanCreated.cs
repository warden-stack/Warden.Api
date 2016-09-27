using System;

namespace Warden.Common.Events.Features
{
    public class UserPaymentPlanCreated : IAuthenticatedEvent
    {
        public string UserId { get; set; }
        public Guid PlanId { get; }
        public string Name { get; }
        public decimal MonthlyPrice { get; }

        protected UserPaymentPlanCreated()
        {
        }

        public UserPaymentPlanCreated(string userId, 
            Guid planId, string name, decimal monthlyPrice)
        {
            UserId = userId;
            PlanId = planId;
            Name = name;
            MonthlyPrice = monthlyPrice;
        }
    }
}