using System.Threading.Tasks;
using Warden.Api.Core.Services;
using Warden.Common.Events;
using Warden.Common.Events.ApiKeys;

namespace Warden.Api.Core.Handlers
{
    public class ApiKeyCreatedHandler : IEventHandler<ApiKeyCreated>
    {
        private readonly IIdentityProvider _identityProvider;

        public ApiKeyCreatedHandler(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public async Task HandleAsync(ApiKeyCreated @event)
        {
            _identityProvider.SetUserIdForApiKey(@event.ApiKey, @event.UserId);
            await Task.CompletedTask;
        }
    }
}