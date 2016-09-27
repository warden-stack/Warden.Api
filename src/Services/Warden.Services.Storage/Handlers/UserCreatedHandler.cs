using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Events;
using Warden.Common.Events.Users;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly IUserRepository _userRepository;

        public UserCreatedHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            var user = await _userRepository.GetByIdAsync(@event.UserId);
            if (user.HasValue)
                return;

            await _userRepository.AddAsync(new UserDto
            {
                UserId = @event.UserId,
                Email = @event.Email,
                Role = @event.Role,
                State = @event.State,
                CreatedAt = @event.CreatedAt
            });
        }
    }
}