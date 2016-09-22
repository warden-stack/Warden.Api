using System;
using System.Threading.Tasks;
using Warden.Common.Commands;

namespace Warden.Services.Spawn.Handlers.Commands
{
    public class SpawnWardenHandler
    {
        public async Task Handle(SpawnWarden message)
        {
            Console.WriteLine("Spawning new Warden...");
            //await _bus.Publish(new RunWardenProcess(message.ConfigurationId, message.Token));
        }
    }
}