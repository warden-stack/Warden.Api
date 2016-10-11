using System;
using System.Linq;
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
        private readonly IWardenChecksCounter _wardenChecksCounter;

        public UserSignedInHandler(IBusClient bus,
            IUserRepository userRepository,
            IUserPaymentPlanService userPaymentPlanService,
            IWardenChecksCounter wardenChecksCounter)
        {
            _bus = bus;
            _userRepository = userRepository;
            _userPaymentPlanService = userPaymentPlanService;
            _wardenChecksCounter = wardenChecksCounter;
        }

        public async Task HandleAsync(UserSignedIn @event)
        {
            var user = await _userRepository.GetAsync(@event.UserId);
            if(user.HasValue)
                return;

            await _userRepository.AddAsync(new User(@event.Email, @event.UserId, @event.Role, @event.State));
            await _userPaymentPlanService.CreateDefaultAsync(@event.UserId);
            var plan = await _userPaymentPlanService.GetCurrentPlanAsync(@event.UserId);
            var addWardenChecksFeature = plan.Value.Features.First(x => x.Type == FeatureType.AddWardenCheck);
            await _wardenChecksCounter.InitializeAsync(@event.UserId, addWardenChecksFeature.Limit);
            await _bus.PublishAsync(new UserPaymentPlanCreated(Guid.NewGuid(), @event.UserId, plan.Value.Id,
                plan.Value.Name, plan.Value.MonthlyPrice));
        }
    }
}