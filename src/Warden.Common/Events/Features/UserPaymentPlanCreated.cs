using System;

namespace Warden.Common.Events.Features
{
    public class UserPaymentPlanCreated : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public string UserId { get; }
        public Guid PlanId { get; }
        public string Name { get; }
        public decimal MonthlyPrice { get; }

        protected UserPaymentPlanCreated()
        {
        }

        public UserPaymentPlanCreated(Guid commandId, string userId, 
            Guid planId, string name, decimal monthlyPrice)
        {
            CommandId = commandId;
            UserId = userId;
            PlanId = planId;
            Name = name;
            MonthlyPrice = monthlyPrice;
        }
    }
}