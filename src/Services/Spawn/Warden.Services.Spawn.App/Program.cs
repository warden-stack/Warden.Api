using System;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Transport.Msmq;
using Rebus.Routing.TypeBased;
using Rebus.Logging;
using Warden.Services.Spawn.Commands;
using Warden.Services.Spawn.Handlers.Commands;

namespace Warden.Services.Spawn.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Console.Title = "Warden.Services.Spawn.App";
                activator.Register((bus, message) => new SpawnWardenHandler(bus));
                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
                    .Transport(t => t.UseMsmq("Warden.Services.Spawn"))
                    .Routing(r => r.TypeBased().Map<SpawnWarden>("Warden.Api"))
                    .Start();

                activator.Bus.Subscribe<SpawnWarden>().Wait();
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }
        }
    }
}
