using Nancy;
using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Messages.Commands.Users;
using Warden.Services.Storage.Models.Users;

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
            Get("", async args => await FetchCollection<BrowseApiKeys, ApiKey>
                (async x => await apiKeyStorage.BrowseAsync(x))
                .MapTo(x => new 
                {
                    Name = x.Name,
                    Key = x.Key
                })
                .HandleAsync());

            Get("{name}", async args => await Fetch<GetApiKey, ApiKey>
                (async x => await apiKeyStorage.GetAsync(x.UserId, x.Name))
                .MapTo(x => new 
                {
                    Name = x.Name,
                    Key = x.Key
                })
                .HandleAsync());

            Post("", async args => await For<RequestNewApiKey>()
                .OnSuccessAccepted("api-keys")
                .DispatchAsync());

            Delete("{name}", async args => await For<DeleteApiKey>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}