using System;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Routing.TypeBased;
using Rebus.Transport.Msmq;
using Warden.Common.Services.Events;
using Warden.Services.Stats.Handlers.Events;

namespace Warden.Services.Stats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Console.Title = "Warden.Services.Stats";
                activator.Register((bus, message) => new WardenCheckResultProcessedHandler(bus));
                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
                    .Transport(t => t.UseMsmq("Warden.Services.Stats"))
                    .Routing(r => r.TypeBased().Map<WardenCheckResultProcessed>("Warden.Services.RealTime"))
                    .Start();

                activator.Bus.Subscribe<WardenCheckResultProcessed>().Wait();
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }
        }
    }
}
