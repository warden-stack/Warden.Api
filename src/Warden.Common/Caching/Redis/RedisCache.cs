using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;
using Warden.Common.Extensions;
using Warden.Common.Types;

namespace Warden.Common.Caching.Redis
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;
        private readonly RedisSettings _settings;

        public RedisCache(IDatabase database, RedisSettings settings)
        {
            _database = database;
            _settings = settings;
        }

        public async Task<Maybe<T>> GetAsync<T>(string key) where T : class
        {
            if (!_settings.Enabled)
                return default(T);

            var fixedKey = key.ToLowerInvariant();
            var value = Deserialize<T>(await _database.StringGetAsync(fixedKey));

            return value;
        }

        public async Task AddAsync(string key, object value, TimeSpan? expiry = null)
        {
            if(!_settings.Enabled)
                return;

            var fixedKey = key.ToLowerInvariant();
            var obj = Serialize(value);
            await _database.StringSetAsync(fixedKey, obj, expiry);
        }

        public async Task DeleteAsync(string key)
        {
            if (!_settings.Enabled)
                return;

            var fixedKey = key.ToLowerInvariant();
            await AddAsync(fixedKey, null, TimeSpan.FromMilliseconds(1));
        }

        private static string Serialize<T>(T value) => JsonConvert.SerializeObject(value);

        private static T Deserialize<T>(string serializedObject)
            => serializedObject.Empty() ? default(T) : JsonConvert.DeserializeObject<T>(serializedObject);
    }
}