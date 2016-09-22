using System;
using Warden.Common.Events;
using Warden.Services.Stats.Handlers.Events;

namespace Warden.Services.Stats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.Stats";
            //activator.Register((bus, message) => new WardenCheckResultProcessedHandler(bus));
            //Configure.With(activator)
            //    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
            //    .Transport(t => t.UseMsmq("Warden.Services.Stats"))
            //    .Routing(r => r.TypeBased().Map<WardenCheckResultProcessed>("Warden.Services.Storage"))
            //    .Start();

            //activator.Bus.Subscribe<WardenCheckResultProcessed>().Wait();
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
