using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.ApiKeys
{
    public class DeleteApiKey : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public string Key { get; set; }
    }

    public class DeleteApiKeyHandler : ICommandHandler<DeleteApiKey>
    {
        private readonly IApiKeyService _apiKeyService;

        public DeleteApiKeyHandler(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        public async Task HandleAsync(DeleteApiKey command)
        {
            await _apiKeyService.DeleteAsync(command.Key);
        }
    }
}