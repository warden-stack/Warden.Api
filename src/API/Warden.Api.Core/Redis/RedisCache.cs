using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;
using Warden.Api.Core.Cache;
using Warden.Common.Types;
using Warden.Common.Extensions;

namespace Warden.Api.Core.Redis
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;

        public RedisCache(IDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<T>> GetAsync<T>(string key) where T : class
        {
            var fixedKey = key.ToLowerInvariant();
            var value = Deserialize<T>(await _database.StringGetAsync(fixedKey));
            return value;
        }

        public async Task AddAsync(string key, object value, TimeSpan? expiry = null)
        {
            var fixedKey = key.ToLowerInvariant();
            var obj = Serialize(value);
            await _database.StringSetAsync(fixedKey, obj, expiry);
        }

        public async Task DeleteAsync(string key)
        {
            var fixedKey = key.ToLowerInvariant();
            await AddAsync(fixedKey, null, TimeSpan.FromMilliseconds(1));
        }

        private static string Serialize<T>(T value) => JsonConvert.SerializeObject(value);

        private static T Deserialize<T>(string serializedObject)
            => serializedObject.Empty() ? default(T) : JsonConvert.DeserializeObject<T>(serializedObject);
    }
}