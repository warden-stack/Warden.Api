using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Services
{
    public interface IUserFeaturesCounter
    {
        Task SetEmptyUsageAsync(string userId, IEnumerable<Feature> features);
        Task<bool> CanUseAsync(string userId, FeatureType feature);
        Task IncreaseUsageAsync(string userId, FeatureType feature);
        Task ResetUsageAsync(string userId, FeatureType feature);
        Task SetUsageAsync(string userId, FeatureType feature, int usage);
    }
}