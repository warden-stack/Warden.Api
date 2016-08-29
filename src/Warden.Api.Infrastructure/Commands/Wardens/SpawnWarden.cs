using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Wardens
{
    public class SpawnWarden : ICommand
    {
        public object Configuration { get; set; }
    }

    public class SpawnWardenHandler : ICommandHandler<SpawnWarden>
    {
        private readonly IWardenService _wardenService;
        private readonly IWardenConfigurationService _wardenConfigurationService;

        public SpawnWardenHandler(IWardenService wardenService, IWardenConfigurationService wardenConfigurationService)
        {
            _wardenService = wardenService;
            _wardenConfigurationService = wardenConfigurationService;
        }

        public async Task HandleAsync(SpawnWarden command)
        {
            await _wardenConfigurationService.CreateWardenAsync(command.Configuration);
        }
    }
}