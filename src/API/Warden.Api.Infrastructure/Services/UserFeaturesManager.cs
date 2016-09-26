using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Services
{
    public class UserFeaturesManager : IUserFeaturesManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPaymentPlanRepository _userPaymentPlanRepository;
        private readonly PaymentPlanSettings _paymentPlanSettings;

        public UserFeaturesManager(IUserRepository userRepository,
            IUserPaymentPlanRepository userPaymentPlanRepository,
            PaymentPlanSettings paymentPlanSettings)
        {
            _userRepository = userRepository;
            _userPaymentPlanRepository = userPaymentPlanRepository;
            _paymentPlanSettings = paymentPlanSettings;
        }

        public async Task UseFeatureIfAvailableAsync(string userId, FeatureType feature, Func<Task> action)
        {
            if (!_paymentPlanSettings.Enabled)
            {
                await action();

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

            await action();
            monthlySubscription.IncreaseFeatureUsage(feature);
            await _userPaymentPlanRepository.UpdateAsync(paymentPlan.Value);
        }
    }
}