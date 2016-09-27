using Nancy;
using Warden.Services.Storage.Providers;

namespace Warden.Services.Storage.Modules
{
    public class ApiKeyModule : NancyModule
    {
        private readonly IApiKeyProvider _apiKeyProvider;

        public ApiKeyModule(IApiKeyProvider apiKeyProvider)
        {
            _apiKeyProvider = apiKeyProvider;

            Get("/users/{userId}/api-keys", async args => await _apiKeyProvider.BrowseAsync((string)args.userId));
        }
    }
}