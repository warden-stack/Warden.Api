using System.Threading.Tasks;
using Warden.Api.Queries;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Storage
{
    public class ApiKeyStorage : IApiKeyStorage
    {
        private readonly IStorageClient _storageClient;

        public ApiKeyStorage(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Maybe<ApiKeyDto>> GetAsync(string userId, string name)
            => await _storageClient.GetUsingCacheAsync<ApiKeyDto>($"users/{userId}/api-keys/{name}");

        public async Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey)
            => await _storageClient.GetUsingCacheAsync<string>($"api-keys/{apiKey}");

        public async Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query)
            => await _storageClient.GetFilteredCollectionAsync<ApiKeyDto, BrowseApiKeys>(query, $"users/{query.UserId}/api-keys");
    }
}