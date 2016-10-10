using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Repositories
{
    public interface IUserPaymentPlanRepository
    {
        Task<Maybe<UserPaymentPlan>> GetAsync(Guid id);
        Task<Maybe<WardenCheckUsageInfo>> GetWardenCheckUsageInfoAsync(string userId);
        Task UpdateAsync(UserPaymentPlan paymentPlan);
        Task AddAsync(UserPaymentPlan paymentPlan);
    }
}