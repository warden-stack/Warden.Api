using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.PaymentPlans;
using Warden.Api.Core.Repositories;

namespace Warden.Api.Infrastructure.Services
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
            var defaultPlan = await _paymentPlanRepository.GetDefaultAsync();
            var userPlan = new UserPaymentPlan(user.Value, defaultPlan);
            await _userPaymentPlanRepository.AddAsync(userPlan);
        }
    }
}