using System.Threading.Tasks;
using Warden.Common.DTO.Features;
using Warden.Common.Types;

namespace Warden.Services.Features.Services
{
    public interface IUserPaymentPlanService
    {
        Task CreateDefaultAsync(string userId);
        Task<Maybe<UserPaymentPlanDto>> GetCurrentPlanAsync(string userId);
    }
}