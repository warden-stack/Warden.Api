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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IApiKeyService _apiKeyService;
        private readonly IBusClient _bus;

        public CreateApiKeyHandler(IApiKeyService apiKeyService,
            IBusClient bus)
        {
            _apiKeyService = apiKeyService;
            _bus = bus;
        }

        public async Task HandleAsync(CreateApiKey command)
        {
            await _apiKeyService.CreateAsync(command.ApiKeyId, command.UserId);
            var apiKey = await _apiKeyService.GetAsync(command.ApiKeyId);
            await _bus.PublishAsync(new ApiKeyCreated(command.Request.Id, command.UserId, apiKey.Value.Key));
        }
    }
}