using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Api.Core.Settings;
using Warden.Common.Extensions;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public class StorageClient : IStorageClient
    {
        private readonly ICache _cache;
        private readonly StorageSettings _settings;
        private readonly HttpClient _httpClient;

        private string BaseAddress
            => _settings.Url.EndsWith("/", StringComparison.CurrentCulture) ? _settings.Url : $"{_settings.Url}/";

        public StorageClient(ICache cache, StorageSettings settings)
        {
            _cache = cache;
            _settings = settings;
            _httpClient = new HttpClient {BaseAddress = new Uri(BaseAddress)};
            _httpClient.DefaultRequestHeaders.Remove("Accept");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<Maybe<T>> GetAsync<T>(string endpoint) where T : class
        {
            if (endpoint.Empty())
                throw new ArgumentException("Endpoint can not be empty.");

            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
                return new Maybe<T>();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        public async Task<Maybe<T>> GetAsyncUsingCache<T>(string endpoint, string cacheKey = null, TimeSpan? expiry = null)
            where T : class
        {
            if(endpoint.Empty())
                throw new ArgumentException("Endpoint can not be empty.");

            if (cacheKey.Empty())
                cacheKey = endpoint;

            var result = await _cache.GetAsync<T>(cacheKey);
            if (result.HasValue)
                return result;

            result = await GetAsync<T>(endpoint);
            if (result.HasNoValue)
                return new Maybe<T>();

            var cacheExpiry = expiry ?? _settings.CacheExpiry;
            await _cache.AddAsync(cacheKey, result.Value, cacheExpiry);

            return result;
        }
    }
}