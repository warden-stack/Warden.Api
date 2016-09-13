using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.ApiKeys
{
    public class CreateApiKey : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public class CreateApiKeyHandler : ICommandHandler<CreateApiKey>
    {
        private readonly IApiKeyService _apiKeyService;

        public CreateApiKeyHandler(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        public async Task HandleAsync(CreateApiKey command)
        {
            await _apiKeyService.CreateAsync(command.Id, command.AuthenticatedUserId);
        }
    }
}