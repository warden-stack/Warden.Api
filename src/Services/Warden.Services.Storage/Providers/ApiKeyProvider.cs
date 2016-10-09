using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;
using Warden.Services.Storage.Queries;
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

        public async Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query)
            => await _providerClient.GetCollectionUsingStorageAsync(_providerSettings.UsersApiUrl,
                "api-keys", async () =>
                {
                    var apiKeys = await _apiKeyRepository.BrowseAsync(query);
                    if (apiKeys.HasValue && apiKeys.Value.IsNotEmpty)
                        return apiKeys;

                    return new Maybe<PagedResult<ApiKeyDto>>();

                }, async keys =>
                {
                    await _apiKeyRepository.AddManyAsync(keys.Items);
                });
    }
}