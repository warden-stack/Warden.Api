using System;

namespace Warden.Common.DTO.Features
{
    public class UserPaymentPlanDto
    {
        public Guid PlanId { get; set; }
        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}