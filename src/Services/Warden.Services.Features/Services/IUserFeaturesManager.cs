using System;
using System.Threading.Tasks;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Services
{
    public interface IUserFeaturesManager
    {
        Task UseFeatureAsync(string userId, FeatureType feature);
        Task<bool> IsFeatureIfAvailableAsync(string userId, FeatureType feature);
    }
}