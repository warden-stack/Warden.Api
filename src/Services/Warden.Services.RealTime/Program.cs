using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using RawRabbit.vNext;
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
            var client = BusClientFactory.CreateDefault();
            client.SubscribeAsync<ProcessWardenCheckResult>(async (msg, context) =>
                new ProcessWardenCheckResultHandler(service)
                    .HandleAsync(msg));
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
