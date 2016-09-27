using System;
using System.Threading.Tasks;
using NLog;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.ApiKeys;
using Warden.Common.Events.ApiKeys;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Handlers
{
    public class CreateApiKeyHandler : ICommandHandler<CreateApiKey>
    {
        private readonly IApiKeyService _apiKeyService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IBusClient _bus;

        public CreateApiKeyHandler(IApiKeyService apiKeyService,
            IBusClient bus)
        {
            _apiKeyService = apiKeyService;
            _bus = bus;
        }

        public async Task HandleAsync(CreateApiKey command)
        {
            var apiKeyId = Guid.NewGuid();
            await _apiKeyService.CreateAsync(apiKeyId, command.AuthenticatedUserId);
            var apiKey = await _apiKeyService.GetAsync(apiKeyId);
            await _bus.PublishAsync(new ApiKeyCreated(command.AuthenticatedUserId, apiKey.Value.Key));
        }
    }
}