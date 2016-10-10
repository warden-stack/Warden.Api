using Warden.Services.Users.Domain;
using Warden.Services.Users.Queries;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Modules
{
    public class ApiKeyModule : ModuleBase
    {
        public ApiKeyModule(IApiKeyService apiKeyService) : base("api-keys")
        {
            Get("", async args => await FetchCollection<BrowseApiKeys, ApiKey>
                (async x => await apiKeyService.BrowseAsync(x)).HandleAsync());
        }
    }
}