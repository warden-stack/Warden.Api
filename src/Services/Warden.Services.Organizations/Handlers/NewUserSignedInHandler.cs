using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Users;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Repositories;

namespace Warden.Services.Organizations.Handlers
{
    public class NewUserSignedInHandler : IEventHandler<NewUserSignedIn>
    {
        private readonly IUserRepository _userRepository;

        public NewUserSignedInHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(NewUserSignedIn @event)
        {
            var user = await _userRepository.GetAsync(@event.UserId);
            if (user.HasValue)
                return;

            await _userRepository.AddAsync(new User(@event.UserId, @event.Email, @event.Role, @event.State));
        }
    }
}