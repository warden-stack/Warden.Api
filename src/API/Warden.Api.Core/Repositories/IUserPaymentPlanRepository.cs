using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Common.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IUserPaymentPlanRepository
    {
        Task<Maybe<UserPaymentPlan>> GetAsync(Guid id);
        Task UpdateAsync(UserPaymentPlan paymentPlan);
        Task AddAsync(UserPaymentPlan paymentPlan);
    }
}