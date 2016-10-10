using System.Threading.Tasks;
using Warden.Api.Core.Filters;
using Warden.Api.Core.Services;
using Warden.Api.Core.Storage;
using Warden.Common.Tasks;

namespace Warden.Api.Tasks
{
    public class SynchronizeApiKeysTask : ITask
    {
        private readonly IApiKeyStorage _apiKeyStorage;
        private readonly IIdentityProvider _identityProvider;

        public SynchronizeApiKeysTask(IApiKeyStorage apiKeyStorage, IIdentityProvider identityProvider)
        {
            _apiKeyStorage = apiKeyStorage;
            _identityProvider = identityProvider;
        }

        public async Task ExecuteAsync()
        {
            //TODO: Fetch all api keys using pagination.
            var apiKeys = await _apiKeyStorage.BrowseAsync(new BrowseApiKeys
            {
                Page = 1,
                Results = 1000
            });
            if(apiKeys.HasNoValue)
                return;
            if(apiKeys.Value.IsEmpty)
                return;

            foreach (var apiKey in apiKeys.Value.Items)
            {
                _identityProvider.SetUserIdForApiKey(apiKey.Key, apiKey.UserId);
            }
        }
    }
}