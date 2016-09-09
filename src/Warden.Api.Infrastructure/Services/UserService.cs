using System.Threading.Tasks;
using AutoMapper;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Events.Users;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.DTO.Users;
using Warden.Api.Infrastructure.Events;

namespace Warden.Api.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEventDispatcher _eventDispatcher;

        public UserService(IUserRepository userRepository, IMapper mapper, IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _eventDispatcher = eventDispatcher;
        }

        public async Task SignInUserAsync(string email, string externalId, string picture)
        {
            if (email.Empty())
                throw new ServiceException("Email cannot be empty");
            if (externalId.Empty())
                throw new ServiceException("ExternalId cannot be empty");
            
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasNoValue)
            {
                await _eventDispatcher.DispatchAsync(new NewUserSignedIn(email, externalId, picture));
                return;
            }

            await _eventDispatcher.DispatchAsync(new UserSignedIn(email));
        }

        public async Task<UserDto> GetAsync(string externalId)
        {
            var user = await _userRepository.GetAsync(externalId);
            if (user.HasNoValue)
                throw new ServiceException($"Desired user does not exist, externalId: {externalId}");

            var result = _mapper.Map<UserDto>(user.Value);

            return result;
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasNoValue)
                throw new ServiceException($"Desired user does not exist, email: {email}");

            var result = _mapper.Map<UserDto>(user.Value);

            return result;
        }

        public async Task CreateAsync(string email, string externalId)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasValue)
                throw new ServiceException($"User with e-mail: {email} already exists");

            await _userRepository.CreateAsync(email, externalId);
            await _eventDispatcher.DispatchAsync(new UserCreated(email, externalId));
        }
    }
}