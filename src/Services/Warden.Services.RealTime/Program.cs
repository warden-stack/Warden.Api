using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Warden.Common.Commands;
using Warden.Services.RealTime.Handlers.Commands;
using Warden.Services.RealTime.Hubs;

namespace Warden.Services.RealTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.RealTime";
            var hub = GlobalHost.ConnectionManager.GetHubContext<WardenHub>();
            var service = new SignalRService(hub);
            //activator.Register((bus, message) => new ProcessWardenCheckResultHandler(service));
            //Configure.With(activator)
            //    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
            //    .Transport(t => t.UseMsmq("Warden.Services.RealTime"))
            //    .Routing(r => r.TypeBased().Map<ProcessWardenCheckResult>("Warden.Api"))
            //    .Start();

            //activator.Bus.Subscribe<ProcessWardenCheckResult>().Wait();
            string url = "http://*:8081";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
