using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Features;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
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
            var user = await _userRepository.GetByIdAsync(@event.UserId);
            if(user.HasNoValue)
                return;
            if(user.Value.PaymentPlanId == @event.PlanId)
                return;

            user.Value.PaymentPlanId = @event.PlanId;
            await _userRepository.UpdateAsync(user.Value);
        }
    }
}