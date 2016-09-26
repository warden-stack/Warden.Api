using System.Threading.Tasks;
using Warden.Api.Core.Cache;
using Warden.Api.Core.Storage;
using Warden.Common.DTO.Users;

namespace Warden.Api.Core.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly ICache _cache;
        private readonly ICacheKeys _cacheKeys;
        private readonly IUserStorage _userStorage;

        public UserProvider(ICache cache, ICacheKeys cacheKeys, 
            IUserStorage userStorage)
        {
            _cache = cache;
            _cacheKeys = cacheKeys;
            _userStorage = userStorage;
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var user = await _cache.GetAsync<UserDto>(_cacheKeys.Users(id));
            if (user.HasValue)
                return user.Value;

            user = await _userStorage.GetAsync(id);

            return user.HasValue ? user.Value : new UserDto();
        }
    }
}