using Nancy;
using System.Linq;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Modules
{
    public class ApiKeyModule : NancyModule
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyModule(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;

            Get("/users/{userId}/api-keys", async args =>
            {
                var apiKeys = await _apiKeyService.BrowseAsync((string)args.userId);
                if (apiKeys.HasValue)
                    return apiKeys.Value.Select(x => x.Key);

                return HttpStatusCode.NotFound;
            });
        }
    }
}