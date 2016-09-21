using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Warden.Common.Commands;

namespace Warden.Services.Spawn.Handlers.Commands
{
    public class SpawnWardenHandler : IHandleMessages<SpawnWarden>
    {
        private readonly IBus _bus;

        public SpawnWardenHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(SpawnWarden message)
        {
            await _bus.Publish(new RunWardenProcess(message.ConfigurationId, message.Token));
        }
    }
}