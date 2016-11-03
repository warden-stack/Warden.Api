using System;
using System.Threading.Tasks;
using Warden.Common.Domain;
using Warden.Common.Types;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories;

namespace Warden.Services.Features.Services
{
    public class UserPaymentPlanService : IUserPaymentPlanService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPaymentPlanRepository _paymentPlanRepository;
        private readonly IUserPaymentPlanRepository _userPaymentPlanRepository;

        public UserPaymentPlanService(IUserRepository userRepository,
            IPaymentPlanRepository paymentPlanRepository, 
            IUserPaymentPlanRepository userPaymentPlanRepository)
        {
            _userRepository = userRepository;
            _paymentPlanRepository = paymentPlanRepository;
            _userPaymentPlanRepository = userPaymentPlanRepository;
        }

        public async Task CreateDefaultAsync(string userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue)
                throw new ArgumentException($"User with id: {userId} has not been found.");

            var defaultPlan = await _paymentPlanRepository.GetDefaultAsync();
            if (defaultPlan.HasNoValue)
                throw new ServiceException("Default payment plan has not been found.");

            var userPlan = new UserPaymentPlan(user.Value, defaultPlan.Value);
            userPlan.AddMonthlySubscription(DateTime.UtcNow, userPlan.Features);
            await _userPaymentPlanRepository.AddAsync(userPlan);
            user.Value.SetPaymentPlan(userPlan);
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task<Maybe<UserPaymentPlan>> GetCurrentPlanAsync(string userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue || !user.Value.PaymentPlanId.HasValue)
                return new Maybe<UserPaymentPlan>();

            return await _userPaymentPlanRepository.GetAsync(user.Value.PaymentPlanId.Value);
        }
    }
}