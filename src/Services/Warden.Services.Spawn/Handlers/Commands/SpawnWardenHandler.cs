using System;
using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;

namespace Warden.Services.Spawn.Handlers.Commands
{
    public class SpawnWardenHandler : ICommandHandler<SpawnWarden>
    {
        private readonly IBusClient _bus;

        public SpawnWardenHandler(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task HandleAsync(SpawnWarden command)
        {
            Console.WriteLine("Spawning new Warden...");
            await _bus.PublishAsync(new RunWardenProcess(command.ConfigurationId, command.Token));
        }
    }
}