using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Api.Storage
{
    public class ApiKeyStorage : IApiKeyStorage
    {
        private readonly IStorageClient _storageClient;

        public ApiKeyStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<ApiKey>> GetAsync(string userId, string name)
            => await _storageClient.GetUsingCacheAsync<ApiKey>($"users/{userId}/api-keys/{name}");

        public async Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey)
            => await _storageClient.GetUsingCacheAsync<string>($"api-keys/{apiKey}");

        public async Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query)
            => await _storageClient.GetFilteredCollectionAsync<ApiKey, BrowseApiKeys>(query, $"api-keys");
    }
}