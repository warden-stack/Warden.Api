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
    public class NewUserSignedInHandler : IEventHandler<NewUserSignedIn>
    {
        private readonly IBusClient _bus;
        private readonly IUserRepository _userRepository;
        private readonly IUserPaymentPlanService _userPaymentPlanService;

        public NewUserSignedInHandler(IBusClient bus,
            IUserRepository userRepository, 
            IUserPaymentPlanService userPaymentPlanService)
        {
            _bus = bus;
            _userRepository = userRepository;
            _userPaymentPlanService = userPaymentPlanService;
        }

        public async Task HandleAsync(NewUserSignedIn @event)
        {
            await _userRepository.AddAsync(new User(@event.Email, @event.UserId, @event.Role));
            await _userPaymentPlanService.CreateDefaultAsync(@event.UserId);
            var plan = await _userPaymentPlanService.GetCurrentPlanAsync(@event.UserId);
            await _bus.PublishAsync(new UserPaymentPlanCreated(@event.UserId, plan.Value.PlanId,
                plan.Value.Name, plan.Value.MonthlyPrice));
        }
    }
}