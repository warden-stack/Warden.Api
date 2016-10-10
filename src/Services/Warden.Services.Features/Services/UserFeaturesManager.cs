using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories;
using Warden.Services.Features.Settings;

namespace Warden.Services.Features.Services
{
    public class UserFeaturesManager : IUserFeaturesManager
    {
        private readonly IWardenChecksCounter _wardenChecksCounter;
        private readonly IUserRepository _userRepository;
        private readonly IUserPaymentPlanRepository _userPaymentPlanRepository;
        private readonly PaymentPlanSettings _paymentPlanSettings;

        public UserFeaturesManager(IWardenChecksCounter wardenChecksCounter,
            IUserRepository userRepository,
            IUserPaymentPlanRepository userPaymentPlanRepository,
            PaymentPlanSettings paymentPlanSettings)
        {
            _wardenChecksCounter = wardenChecksCounter;
            _userRepository = userRepository;
            _userPaymentPlanRepository = userPaymentPlanRepository;
            _paymentPlanSettings = paymentPlanSettings;
        }

        public async Task<bool> IsFeatureIfAvailableAsync(string userId, FeatureType feature)
        {
            if (!_paymentPlanSettings.Enabled)
                return true;

            var initializeWardenChecksUsage = false;
            if (feature == FeatureType.AddWardenCheck)
            {
                var wardenChecksUsageInitialized = await _wardenChecksCounter.IsInitializedAsync(userId);
                if (!wardenChecksUsageInitialized)
                    initializeWardenChecksUsage = true;
            }

            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue)
                return false;
            if (!user.Value.PaymentPlanId.HasValue)
                return false;
            var paymentPlan = await _userPaymentPlanRepository.GetAsync(user.Value.PaymentPlanId.Value);
            if (paymentPlan.HasNoValue)
                return false;
            var monthlySubscription = paymentPlan.Value.GetMonthlySubscription(DateTime.UtcNow);
            if (monthlySubscription == null)
                return false;
            if (!monthlySubscription.CanUseFeature(feature))
                return false;
            if (!initializeWardenChecksUsage)
                return true;

            var wardenCheckFeature = monthlySubscription.FeatureUsages.First(x => x.Feature == FeatureType.AddWardenCheck);
            await _wardenChecksCounter.InitializeAsync(userId, wardenCheckFeature.Limit);

            return true;
        }

        public async Task IncreaseFeatureUsageAsync(string userId, FeatureType feature)
        {
            if (!_paymentPlanSettings.Enabled)
                return;

            if (feature == FeatureType.AddWardenCheck)
            {
                var canUseWardenCheckAsync = await _wardenChecksCounter.CanUseAsync(userId);
                if (!canUseWardenCheckAsync)
                    return;

                await _wardenChecksCounter.IncreaseUsageAsync(userId);
                var usage = await _wardenChecksCounter.GetUsageAsync(userId);
                if (usage%_paymentPlanSettings.FlushWardenChecksLimit != 0)
                    return;
            }

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
            if (!monthlySubscription.CanUseFeature(feature))
                throw new InvalidOperationException($"Feature {feature} has reached its limit.");

            monthlySubscription.IncreaseFeatureUsage(feature);
            await _userPaymentPlanRepository.UpdateAsync(paymentPlan.Value);
        }
    }
}