using System;
using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.ApiKeys;
using Warden.DTO.ApiKeys;
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
            var apiKey = await _apiKeyRepository.GetAsync(@event.ApiKey);
            if (apiKey.HasValue)
                return;

            await _apiKeyRepository.AddAsync(new ApiKeyDto
            {
                Id = Guid.NewGuid(),
                UserId = @event.UserId,
                Key = @event.ApiKey
            });
        }
    }
}