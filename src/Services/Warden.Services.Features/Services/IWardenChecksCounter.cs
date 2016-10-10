using System.Threading.Tasks;

namespace Warden.Services.Features.Services
{
    public interface IWardenChecksCounter
    {
        Task<bool> IsInitializedAsync(string userId);
        Task InitializeAsync(string userId, int limit);
        Task<bool> CanUseAsync(string userId);
        Task IncreaseUsageAsync(string userId);
        Task<int> GetUsageAsync(string userId);
        Task ResetUsageAsync(string userId);
        Task SetUsageAsync(string userId, int usage);
    }
}