using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Services
{
    public interface IUserPaymentPlanService
    {
        Task CreateDefaultAsync(string userId);
        Task<Maybe<UserPaymentPlan>> GetCurrentPlanAsync(string userId);
    }
}