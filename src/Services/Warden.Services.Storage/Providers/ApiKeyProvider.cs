using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Types;
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

        public async Task<Maybe<IEnumerable<string>>> BrowseAsync(string userId) =>
            await _providerClient.GetUsingStorageAsync(_providerSettings.UsersApiUrl,
                $"/users/{userId}/api-keys", async () =>
                {
                    var apiKeys = await _apiKeyRepository.BrowseAsync(userId);

                    return apiKeys.HasValue && apiKeys.Value.Any()
                        ? apiKeys.Value.Select(x => x.Key).ToList()
                        : new Maybe<IEnumerable<string>>();

                }, async keys =>
                {
                    await _apiKeyRepository.AddManyAsync(keys.Select(x => new ApiKeyDto
                    {
                        Id = Guid.NewGuid(),
                        Key = x,
                        UserId = userId
                    }));
                });
    }
}