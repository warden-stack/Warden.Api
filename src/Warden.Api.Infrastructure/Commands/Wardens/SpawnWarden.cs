using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Wardens
{
    public class SpawnWarden : ICommand
    {
        public string Configuration { get; set; }
    }

    public class SpawnWardenHandler : ICommandHandler<SpawnWarden>
    {
        private readonly IWardenService _wardenService;
        private readonly IWardenSpawnService _wardenSpawnService;

        public SpawnWardenHandler(IWardenService wardenService, IWardenSpawnService wardenSpawnService)
        {
            _wardenService = wardenService;
            _wardenSpawnService = wardenSpawnService;
        }

        public async Task HandleAsync(SpawnWarden command)
        {
            await _wardenSpawnService.CreateWardenAsync(command.Configuration);
        }
    }
}