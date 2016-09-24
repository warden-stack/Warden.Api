using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Warden.Services.Commands;
using Warden.Services.Host;
using Warden.Services.Spawn.Framework;

namespace Warden.Services.Spawn
{
    public class Program
    {
        private static readonly string Name = "Warden.Services.Spawn";

        public static void Main(string[] args)
        {
            Console.Title = Name;
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:5002")
                .Build();

            using (var scope = Bootstrapper.LifetimeScope.BeginLifetimeScope())
            {
                var autofacResolver = new AutofacResolver(scope);
                var serviceHost = ServiceHost
                    .Create(Name)
                    .WithResolver(autofacResolver)
                    .WithWebHost(webHost)
                    .WithBus()
                    .SubscribeToCommand<SpawnWarden>()
                    .Build();
                serviceHost.Run();
            }
        }
    }
}
