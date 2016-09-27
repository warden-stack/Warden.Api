using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Features;
using Warden.Services.Users.Repositories;

namespace Warden.Services.Users.Handlers
{
    public class UserPaymentPlanCreatedHandler : IEventHandler<UserPaymentPlanCreated>
    {
        private readonly IUserRepository _userRepository;

        public UserPaymentPlanCreatedHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UserPaymentPlanCreated @event)
        {
            var user = await _userRepository.GetAsync(@event.UserId);
            if (user.HasNoValue)
                return;
            if (user.Value.PaymentPlanId == @event.PlanId)
                return;

            user.Value.SetPaymentPlanId(@event.PlanId);
            await _userRepository.UpdateAsync(user.Value);
        }
    }
}