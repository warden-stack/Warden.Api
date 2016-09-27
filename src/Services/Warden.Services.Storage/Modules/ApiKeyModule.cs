using System.Linq;
using Nancy;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Modules
{
    public class ApiKeyModule : NancyModule
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyModule(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;

            Get("/users/{userId}/api-keys", async args =>
            {
                var apiKeys = await _apiKeyRepository.BrowseAsync((string)args.userId);

                return apiKeys.Select(x => x.Key);
            });
        }
    }
}