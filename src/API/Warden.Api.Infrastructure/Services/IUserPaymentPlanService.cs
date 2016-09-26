using System;
using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUserPaymentPlanService
    {
        Task CreateDefaultAsync(string userId);
    }
}