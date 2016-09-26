using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Infrastructure.Commands.Handlers
{
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