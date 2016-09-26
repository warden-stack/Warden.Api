using System;
using System.Threading.Tasks;

namespace Warden.Services.WardenChecks.Services
{
    public interface IUserFeaturesManager
    {
        Task UseFeatureIfAvailableAsync(Guid userId, FeatureType feature, Func<Task> action);
    }
}