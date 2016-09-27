using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;

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
            => await _storageClient.GetAsync<string>($"api-keys/{apiKey}");

        public async Task<Maybe<IEnumerable<string>>> BrowseAsync(string userId)
            => await _storageClient.GetAsync<IEnumerable<string>>($"users/{userId}/api-keys");
    }
}