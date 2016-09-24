using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Warden.Services.Commands;
using Warden.Services.Host;
using Warden.Services.Storage.Framework;

namespace Warden.Services.Storage
{
    public class Program
    {
        private static readonly string Name = "Warden.Services.Storage";

        public static void Main(string[] args)
        {
            Console.Title = Name;
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
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
