using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Modules
{
    public class ApiKeyModule : ModuleBase
    {
        public ApiKeyModule(IApiKeyProvider apiKeyProvider) : base("api-keys")
        {
            Get("", async args =>
            {
                var query = BindRequest<BrowseApiKeys>();
                var apiKeys = await apiKeyProvider.BrowseAsync(query);

                return FromPagedResult(apiKeys);
            });
        }
    }
}