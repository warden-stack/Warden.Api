using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Features;
using Warden.Common.Types;
using Warden.Services.Domain;
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

        public async Task<Maybe<UserPaymentPlanDto>> GetCurrentPlanAsync(string userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue || !user.Value.PaymentPlanId.HasValue)
                return new Maybe<UserPaymentPlanDto>();

            var plan = await _userPaymentPlanRepository.GetAsync(user.Value.PaymentPlanId.Value);
            if (plan.HasNoValue)
                return new Maybe<UserPaymentPlanDto>();

            return new UserPaymentPlanDto
            {
                PlanId = plan.Value.Id,
                Name = plan.Value.Name,
                MonthlyPrice = plan.Value.MonthlyPrice
            };
        }
    }
}