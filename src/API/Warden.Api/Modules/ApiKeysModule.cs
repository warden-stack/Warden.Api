using Nancy;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Queries;
using Warden.Api.Core.Services;
using Warden.Api.Core.Storage;
using Warden.Common.Commands.ApiKeys;
using Warden.DTO.ApiKeys;

namespace Warden.Api.Modules
{
    public class ApiKeysModule : ModuleBase
    {
        public ApiKeysModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider,
            IApiKeyStorage apiKeyStorage)
            : base(commandDispatcher, identityProvider, modulePath: "api-keys")
        {
            Get("", async args => await FetchCollection<BrowseApiKeys, ApiKeyDto>
                (async x => await apiKeyStorage.BrowseAsync(x)).HandleAsync());

            Post("", async args => await For<RequestNewApiKey>()
                .SetResourceId(x => x.ApiKeyId)
                .OnSuccessAccepted("api-keys/{0}")
                .DispatchAsync());

            Delete("", async args => await For<DeleteApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}