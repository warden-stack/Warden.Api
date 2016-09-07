using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Services
{
    public class UserFeaturesManager : IUserFeaturesManager
    {
        private readonly PaymentPlanSettings _paymentPlanSettings;

        public UserFeaturesManager(PaymentPlanSettings paymentPlanSettings)
        {
            _paymentPlanSettings = paymentPlanSettings;
        }

        public async Task UseFeatureIfAvailableAsync(Guid userId, FeatureType feature)
        {
            if(!_paymentPlanSettings.Enabled)
                return;

            throw new NotImplementedException();
        }
    }
}