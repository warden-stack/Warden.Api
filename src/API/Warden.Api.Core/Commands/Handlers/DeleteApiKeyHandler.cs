using System.Threading.Tasks;
using Warden.Api.Core.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Core.Commands.Handlers
{
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