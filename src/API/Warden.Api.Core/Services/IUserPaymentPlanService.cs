using System.Threading.Tasks;

namespace Warden.Api.Core.Services
{
    public interface IUserPaymentPlanService
    {
        Task CreateDefaultAsync(string userId);
    }
}