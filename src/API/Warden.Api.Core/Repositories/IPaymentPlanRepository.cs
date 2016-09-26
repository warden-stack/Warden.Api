using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Common.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IPaymentPlanRepository
    {
        Task<PaymentPlan> GetDefaultAsync();
        Task<Maybe<PaymentPlan>> GetAsync(Guid id);
        Task<IEnumerable<PaymentPlan>> GetAllAsync();
    }
}