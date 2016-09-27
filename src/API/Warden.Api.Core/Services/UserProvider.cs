using System.Threading.Tasks;
using Warden.Api.Core.Cache;
using Warden.Api.Core.Storage;
using Warden.Common.DTO.Users;

namespace Warden.Api.Core.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly IApiCache _apiCache;
        private readonly IUserStorage _userStorage;
        private readonly IApiKeyStorage _apiKeyStorage;

        public UserProvider(IApiCache apiCache,
            IUserStorage userStorage,
            IApiKeyStorage apiKeyStorage)
        {
            _apiCache = apiCache;
            _userStorage = userStorage;
            _apiKeyStorage = apiKeyStorage;
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var user = await _apiCache.GetUserAsync(id);
            if (user.HasValue)
                return user.Value;

            user = await _userStorage.GetAsync(id);

            return user.HasValue ? user.Value : new UserDto();
        }

        public async Task<string> GetUserIdForApiKeyAsync(string apiKey)
        {
            var userId = await _apiCache.GetUserIdForApiKeyAsync(apiKey);
            if (userId.HasValue)
                return userId.Value;

            userId = await _apiKeyStorage.GetUserIdForApiKeyAsync(apiKey);

            return userId.HasValue ? userId.Value : string.Empty;
        }
    }
}