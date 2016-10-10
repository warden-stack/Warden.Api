using Warden.DTO.ApiKeys;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Modules
{
    public class ApiKeyModule : ModuleBase
    {
        public ApiKeyModule(IApiKeyProvider apiKeyProvider) : base("api-keys")
        {
            Get("", async args => await FetchCollection<BrowseApiKeys, ApiKeyDto>
                (async x => await apiKeyProvider.BrowseAsync(x)).HandleAsync());
        }
    }
}