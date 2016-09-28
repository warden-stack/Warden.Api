using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Common.DTO.ApiKeys;
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
            if (apiKeysResponse.HasNoValue)
                return Enumerable.Empty<string>();

            await _apiKeyRepository.AddManyAsync(apiKeysResponse.Value.Select(x => new ApiKeyDto
            {
                Id = Guid.NewGuid(),
                Key = x,
                UserId = userId
            }));

            return apiKeysResponse.Value;
        }
    }
}