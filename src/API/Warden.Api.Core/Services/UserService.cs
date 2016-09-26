using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Warden.Api.Core.Auth0;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Events;
using Warden.Api.Core.Repositories;
using Warden.Common.DTO.Users;
using Warden.Common.Events.Users;
using Warden.Common.Extensions;

namespace Warden.Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth0RestClient _auth0RestClient;
        private readonly IMapper _mapper;
        private readonly IEventDispatcher _eventDispatcher;

        public UserService(IUserRepository userRepository, IAuth0RestClient auth0RestClient, 
            IMapper mapper, IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _auth0RestClient = auth0RestClient;
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

        public async Task<UserDto> GetByAccessTokenAsync(string accessToken)
        {
            var auth0User = await _auth0RestClient.GetUserByAccessTokenAsync(accessToken);
            if (auth0User == null)
                return null;

            var user = await _userRepository.GetByEmailAsync(auth0User.Email);

            return user.HasValue ? new UserDto
            {
                Id = user.Value.ExternalId,
                Email = user.Value.Email,
                State = user.Value.State.ToString(),
                Role = user.Value.Role.ToString(),
                ApiKeys = new List<string>(),
                RecentlyViewedOrganizationId = user.Value.RecentlyViewedOrganizationId,
                RecentlyViewedWardenId = user.Value.RecentlyViewedWardenId
            } : null;
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasNoValue)
                throw new ServiceException($"Desired user does not exist, email: {email}");

            var result = _mapper.Map<UserDto>(user.Value);

            return result;
        }

        public async Task CreateAsync(string email, string externalId, bool activate = true)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user.HasValue)
                throw new ServiceException($"User with e-mail: {email} already exists");

            user = new User(email, externalId: externalId);
            if (activate)
                user.Value.Activate();

            await _userRepository.AddAsync(user.Value);
            await _eventDispatcher.DispatchAsync(new UserCreated(email, externalId));
        }
    }
}