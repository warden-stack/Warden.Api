using System;
using System.Threading.Tasks;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories;
using Warden.Services.Features.Settings;

namespace Warden.Services.Features.Services
{
    public class UserFeaturesManager : IUserFeaturesManager
    {
        private readonly IUserFeaturesCounter _userFeaturesCounter;
        private readonly IUserRepository _userRepository;
        private readonly IUserPaymentPlanRepository _userPaymentPlanRepository;
        private readonly PaymentPlanSettings _paymentPlanSettings;

        public UserFeaturesManager(IUserFeaturesCounter userFeaturesCounter,
            IUserRepository userRepository,
            IUserPaymentPlanRepository userPaymentPlanRepository,
            PaymentPlanSettings paymentPlanSettings)
        {
            _userFeaturesCounter = userFeaturesCounter;
            _userRepository = userRepository;
            _userPaymentPlanRepository = userPaymentPlanRepository;
            _paymentPlanSettings = paymentPlanSettings;
        }

        public async Task UseFeatureAsync(string userId, FeatureType feature)
        {
            if (!_paymentPlanSettings.Enabled)
                return;

            await _userFeaturesCounter.IncreaseUsageAsync(userId, feature);
            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue)
                throw new ArgumentException($"User {userId} has not been found.");
            if (!user.Value.PaymentPlanId.HasValue)
                throw new InvalidOperationException($"User {userId} has no payment plan assigned.");
            var paymentPlan = await _userPaymentPlanRepository.GetAsync(user.Value.PaymentPlanId.Value);
            if (paymentPlan.HasNoValue)
                throw new InvalidOperationException($"User {userId} payment plan has not been found.");
            var monthlySubscription = paymentPlan.Value.GetMonthlySubscription(DateTime.UtcNow);
            if (monthlySubscription == null)
                throw new InvalidOperationException($"User {userId} monthly subscription has not been found.");
            //if (!monthlySubscription.CanUseFeature(feature))
            //    throw new InvalidOperationException($"Feature {feature} has reached its limit.");

            monthlySubscription.IncreaseFeatureUsage(feature);
            await _userPaymentPlanRepository.UpdateAsync(paymentPlan.Value);
        }

        public async Task<bool> IsFeatureIfAvailableAsync(string userId, FeatureType feature)
        {
            if (!_paymentPlanSettings.Enabled)
                return true;

            return await _userFeaturesCounter.CanUseAsync(userId, feature);

            //var user = await _userRepository.GetAsync(userId);
            //if (user.HasNoValue)
            //    return false;
            //if (!user.Value.PaymentPlanId.HasValue)
            //    return false;
            //var paymentPlan = await _userPaymentPlanRepository.GetAsync(user.Value.PaymentPlanId.Value);
            //if (paymentPlan.HasNoValue)
            //    return false;
            //var monthlySubscription = paymentPlan.Value.GetMonthlySubscription(DateTime.UtcNow);
            //if (monthlySubscription == null)
            //    return false;
            //if (!monthlySubscription.CanUseFeature(feature))
            //    return false;

            //return true;
        }
    }
}