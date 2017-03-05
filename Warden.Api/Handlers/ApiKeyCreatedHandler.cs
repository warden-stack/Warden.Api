using System.Threading.Tasks;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Messages.Events;
using Warden.Messages.Events.Users;

namespace Warden.Api.Handlers
{
    public class ApiKeyCreatedHandler : IEventHandler<ApiKeyCreated>
    {
        private readonly IApiKeyStorage _apiKeyStorage;
        private readonly IIdentityProvider _identityProvider;

        public ApiKeyCreatedHandler(IApiKeyStorage apiKeyStorage, IIdentityProvider identityProvider)
        {
            _apiKeyStorage = apiKeyStorage;
            _identityProvider = identityProvider;
        }

        public async Task HandleAsync(ApiKeyCreated @event)
        {
            var apiKey = await _apiKeyStorage.GetAsync(@event.UserId, @event.Name);
            if (apiKey.HasNoValue)
            {
                return;
            }

           _identityProvider.SetUserIdForApiKey(apiKey.Value.Key, @event.UserId);
        }
    }
}