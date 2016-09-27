using System.Threading.Tasks;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Events;
using Warden.Common.Events.ApiKeys;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class ApiKeyCreatedHandler : IEventHandler<ApiKeyCreated>
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyCreatedHandler(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task HandleAsync(ApiKeyCreated @event)
        {
            await _apiKeyRepository.AddAsync(new ApiKeyDto
            {
                UserId = @event.AuthenticatedUserId,
                Key = @event.ApiKey
            });
        }
    }
}