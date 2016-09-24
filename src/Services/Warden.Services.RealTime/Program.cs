using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Owin.Hosting;
using RawRabbit.vNext;
using Warden.Services.Commands;
using Warden.Services.Extensions;
using Warden.Services.Host;
using Warden.Services.RealTime.Framework;
using Warden.Services.RealTime.Handlers.Commands;
using Warden.Services.RealTime.Hubs;

namespace Warden.Services.RealTime
{
    public class Program
    {
        private static readonly string Name = "Warden.Services.RealTime";

        public static void Main(string[] args)
        {
            Console.Title = Name;
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:5003")
                .Build();

            using (var scope = Bootstrapper.LifetimeScope.BeginLifetimeScope())
            {
                var autofacResolver = new AutofacResolver(scope);
                var serviceHost = ServiceHost
                    .Create(Name)
                    .WithResolver(autofacResolver)
                    .WithWebHost(webHost)
                    .WithBus()
                    .SubscribeToCommand<ProcessWardenCheckResult>()
                    .Build();
                serviceHost.Run();
            }
        }
    }
}
