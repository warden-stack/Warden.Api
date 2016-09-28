using System.Collections.Generic;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Storage;
using Warden.Api.Modules.Base;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Modules
{
    public class ApiKeysModule : AuthenticatedModule
    {
        private readonly IApiKeyStorage _apiKeyStorage;

        public ApiKeysModule(ICommandDispatcher commandDispatcher,
            IApiKeyStorage apiKeyStorage) 
            : base(commandDispatcher, modulePath: "api-keys")
        {
            _apiKeyStorage = apiKeyStorage;
            Get("/", async args =>
            {
                var apiKeys = await _apiKeyStorage.BrowseAsync(CurrentUserId);

                return apiKeys.HasValue ? apiKeys.Value : new List<string>();
            });

            Post("/", async args =>
            {
                var command = BindAuthenticatedCommand<RequestNewApiKey>();
                await CommandDispatcher.DispatchAsync(command);
            });

            Delete("/", async args =>
            {
                var command = BindAuthenticatedCommand<DeleteApiKey>();
                await CommandDispatcher.DispatchAsync(command);
            });
        }
    }
}