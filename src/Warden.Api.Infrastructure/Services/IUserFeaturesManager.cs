using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;

namespace Warden.Api.Infrastructure.Services
{
    public interface IUserFeaturesManager
    {
        Task UseFeatureIfAvailableAsync(Guid userId, FeatureType feature);
    }
}