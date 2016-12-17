using Nancy;
using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Services.Users.Shared.Commands;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Modules
{
    public class ApiKeysModule : ModuleBase
    {
        public ApiKeysModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider,
            IValidatorResolver validatorResolver,
            IApiKeyStorage apiKeyStorage)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "api-keys")
        {
            Get("", async args => await FetchCollection<BrowseApiKeys, ApiKeyDto>
                (async x => await apiKeyStorage.BrowseAsync(x)).HandleAsync());

            Post("", async args => await For<RequestNewApiKey>()
                .OnSuccessAccepted("api-keys")
                .DispatchAsync());

            Delete("", async args => await For<DeleteApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}