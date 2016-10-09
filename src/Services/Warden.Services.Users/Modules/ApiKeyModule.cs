using Warden.Services.Users.Queries;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Modules
{
    public class ApiKeyModule : ModuleBase
    {
        public ApiKeyModule(IApiKeyService apiKeyService) : base("api-keys")
        {
            Get("", async args =>
            {
                var query = BindRequest<BrowseApiKeys>();
                var apiKeys = await apiKeyService.BrowseAsync(query);

                return FromPagedResult(apiKeys);
            });
        }
    }
}