using Nancy;
using Nancy.Security;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Filters;
using Warden.Api.Core.Services;
using Warden.Api.Core.Storage;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Modules
{
    public class ApiKeysModule : ModuleBase
    {

        public ApiKeysModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider,
            IApiKeyStorage apiKeyStorage)
            : base(commandDispatcher, identityProvider, modulePath: "api-keys")
        {
            Get("", async args =>
            {
                this.RequiresAuthentication();
                var apiKeys = await apiKeyStorage.BrowseAsync(new BrowseApiKeys
                {
                    UserId = CurrentUserId
                });

                return FromPagedResult(apiKeys);
            });

            Post("", async args => await For<RequestNewApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());

            Delete("", async args => await For<DeleteApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}