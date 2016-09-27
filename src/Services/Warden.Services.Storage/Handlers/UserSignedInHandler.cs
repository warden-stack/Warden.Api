using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Common;
using Warden.Common.DTO.Users;
using Warden.Common.Events;
using Warden.Common.Events.Users;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class UserSignedInHandler : IEventHandler<UserSignedIn>
    {
        private readonly IUserRepository _userRepository;

        public UserSignedInHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UserSignedIn @event)
        {
            var user = await _userRepository.GetByIdAsync(@event.UserId);
            if(user.HasValue)
                return;

            await _userRepository.AddAsync(new UserDto
            {
                UserId = @event.UserId,
                Email = @event.Email,
                Role = @event.Role,
                State = State.Active,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}