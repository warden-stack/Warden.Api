using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Repositories
{
    public interface IPaymentPlanRepository
    {
        Task<Maybe<PaymentPlan>> GetDefaultAsync();
        Task<Maybe<PaymentPlan>> GetAsync(Guid id);
        Task<IEnumerable<PaymentPlan>> GetAllAsync();
    }
}