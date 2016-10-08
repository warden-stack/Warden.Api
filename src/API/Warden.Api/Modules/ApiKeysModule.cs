using Nancy;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Storage;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Modules
{
    public class ApiKeysModule : ModuleBase
    {

        public ApiKeysModule(ICommandDispatcher commandDispatcher,
            IApiKeyStorage apiKeyStorage)
            : base(commandDispatcher, modulePath: "api-keys")
        {
            Get("/", async args =>
            {
                var apiKeys = await apiKeyStorage.BrowseAsync(CurrentUserId);

                return FromPagedResult(apiKeys);
            });

            Post("/", async args => await For<RequestNewApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());

            Delete("/", async args => await For<DeleteApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}