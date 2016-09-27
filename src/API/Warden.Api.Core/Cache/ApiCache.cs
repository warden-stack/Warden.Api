using System.Threading.Tasks;
using Warden.Common.DTO.Users;
using Warden.Common.Types;

namespace Warden.Api.Core.Cache
{
    public class ApiCache : IApiCache
    {
        private readonly ICache _cache;
        private string Users(string id) => $"users:{id}";
        private string ApiKeys(string apiKey) => $"api-keys:{apiKey}";

        public ApiCache(ICache cache)
        {
            _cache = cache;
        }

        public async Task<Maybe<UserDto>> GetUserAsync(string id)
            => await _cache.GetAsync<UserDto>(Users(id));

        public async Task SetUserAsync(string id, UserDto user)
            => await _cache.AddAsync(Users(id), user);

        public async Task SetUserIdForApiKeyAsync(string userId, string apiKey)
            => await _cache.AddAsync(ApiKeys(apiKey), userId);

        public async Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey)
            => await _cache.GetAsync<string>(ApiKeys(apiKey));
    }
}