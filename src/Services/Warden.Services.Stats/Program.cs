using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Warden.Services.Events;
using Warden.Services.Host;
using Warden.Services.Stats.Framework;

namespace Warden.Services.Stats
{
    public class Program
    {
        private static readonly string Name = "Warden.Services.Stats";

        public static void Main(string[] args)
        {
            Console.Title = Name;
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:5001")
                .Build();

            using (var scope = Bootstrapper.LifetimeScope.BeginLifetimeScope())
            {
                var autofacResolver = new AutofacResolver(scope);
                var serviceHost = ServiceHost
                    .Create(Name)
                    .WithResolver(autofacResolver)
                    .WithWebHost(webHost)
                    .WithBus()
                    .SubscribeToEvent<WardenCheckResultProcessed>()
                    .Build();
                serviceHost.Run();
            }
        }
    }
}
