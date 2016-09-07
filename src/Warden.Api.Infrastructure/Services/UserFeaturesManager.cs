using System;
using System.Threading.Tasks;
using MongoDB.Driver;
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

        public async Task UseFeatureIfAvailableAsync(Guid userId, FeatureType feature)
        {
            if (!_paymentPlanSettings.Enabled)
                return;

            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue)
                throw new ArgumentException($"User has not been found for id {userId}");

            if (!user.Value.PaymentPlanId.HasValue)
                throw new InvalidOperationException($"User {userId} has no payment plan assigned.");

            var paymentPlan = await _userPaymentPlanRepository.GetAsync(user.Value.PaymentPlanId.Value);
            if (paymentPlan.HasNoValue)
                throw new InvalidOperationException($"User {userId} payment plan has not been found.");

            var monthlySubscription = paymentPlan.Value.GetMonthlySubscription(DateTime.UtcNow);
            if (monthlySubscription == null)
                throw new InvalidOperationException($"User {userId} monthly subscription has not been found.");

            monthlySubscription.IncreaseFeatureUsage(feature);
        }
    }
}