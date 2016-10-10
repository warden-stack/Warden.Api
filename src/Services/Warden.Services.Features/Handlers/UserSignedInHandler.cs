using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Events;
using Warden.Common.Events.Features;
using Warden.Common.Events.Users;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Repositories;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class UserSignedInHandler : IEventHandler<UserSignedIn>
    {
        private readonly IBusClient _bus;
        private readonly IUserRepository _userRepository;
        private readonly IUserPaymentPlanService _userPaymentPlanService;
        private readonly IUserFeaturesCounter _userFeaturesCounter;

        public UserSignedInHandler(IBusClient bus,
            IUserRepository userRepository,
            IUserPaymentPlanService userPaymentPlanService,
            IUserFeaturesCounter userFeaturesCounter)
        {
            _bus = bus;
            _userRepository = userRepository;
            _userPaymentPlanService = userPaymentPlanService;
            _userFeaturesCounter = userFeaturesCounter;
        }

        public async Task HandleAsync(UserSignedIn @event)
        {
            var user = await _userRepository.GetAsync(@event.UserId);
            if(user.HasValue)
                return;

            await _userRepository.AddAsync(new User(@event.Email, @event.UserId, @event.Role, @event.State));
            await _userPaymentPlanService.CreateDefaultAsync(@event.UserId);
            var plan = await _userPaymentPlanService.GetCurrentPlanAsync(@event.UserId);
            await _userFeaturesCounter.SetEmptyUsageAsync(@event.UserId, plan.Value.Features);
            await _bus.PublishAsync(new UserPaymentPlanCreated(@event.UserId, plan.Value.Id,
                plan.Value.Name, plan.Value.MonthlyPrice));
        }
    }
}