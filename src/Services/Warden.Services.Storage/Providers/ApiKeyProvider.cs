using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.Settings;

namespace Warden.Services.Storage.Providers
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly IProviderClient _providerClient;
        private readonly ProviderSettings _providerSettings;

        public ApiKeyProvider(IApiKeyRepository apiKeyRepository,
            IProviderClient providerClient,
            ProviderSettings providerSettings)
        {
            _apiKeyRepository = apiKeyRepository;
            _providerClient = providerClient;
            _providerSettings = providerSettings;
        }

        public async Task<IEnumerable<string>> BrowseAsync(string userId)
        {
            var apiKeys = await _apiKeyRepository.BrowseAsync(userId);
            if (apiKeys.Any())
                return apiKeys.Select(x => x.Key);

            var apiKeysResponse = await _providerClient.GetAsync<IEnumerable<string>>(
                _providerSettings.UsersApiUrl, $"/users/{userId}/api-keys");

            return apiKeysResponse.HasValue ? apiKeysResponse.Value : Enumerable.Empty<string>();
        }
    }
}