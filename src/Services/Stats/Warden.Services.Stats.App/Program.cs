using System;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Transport.Msmq;
using Rebus.Routing.TypeBased;
using Rebus.Logging;
using Warden.Services.RealTime.Events;
using Warden.Services.Stats.Handlers.Events;

namespace Warden.Services.Stats.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Console.Title = "Warden.Services.Stats.App";
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
