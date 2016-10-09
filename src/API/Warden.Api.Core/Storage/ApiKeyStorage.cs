using System.Threading.Tasks;
using Warden.Api.Core.Filters;
using Warden.Common.Types;
using Warden.DTO.ApiKeys;

namespace Warden.Api.Core.Storage
{
    public class ApiKeyStorage : IApiKeyStorage
    {
        private readonly IStorageClient _storageClient;

        public ApiKeyStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey)
            => await _storageClient.GetUsingCacheAsync<string>($"api-keys/{apiKey}");

        public async Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query)
            => await _storageClient.GetFilteredCollection<ApiKeyDto, BrowseApiKeys>(query, "api-keys");
    }
}