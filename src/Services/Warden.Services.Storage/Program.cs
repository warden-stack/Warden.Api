using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Routing.TypeBased;
using Rebus.Transport.Msmq;
using Warden.Common.Commands;
using Warden.Common.Events;
using Warden.Services.Storage.Handlers.Commands;
using Warden.Services.Storage.Rethink;

namespace Warden.Services.Storage
{
    public class Program
    {
        private static IConfiguration _configuration;

        public static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Console.Title = "Warden.Services.Storage";
                var env = "dev";
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .AddEnvironmentVariables();
                _configuration = builder.Build();
                var settings = GetConfigurationValue<RethinkDbSettings>("rethinkDb");
                var storage = new RethinkDbWardenCheckStorage(settings);
                activator.Register((bus, message) => new ProcessWardenCheckResultHandler(storage, bus));
                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Debug))
                    .Transport(t => t.UseMsmq("Warden.Services.Storage"))
                    .Routing(r => r.TypeBased().Map<ProcessWardenCheckResult>("Warden.Api"))
                    .Start();

                activator.Bus.Subscribe<ProcessWardenCheckResult>().Wait();
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }
        }

        private static T GetConfigurationValue<T>(string section) where T : new()
        {
            var configurationValue = new T();
            _configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}
