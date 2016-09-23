using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Warden.Common.Commands;
using Warden.Common.Events;
using Warden.Services.Storage.Handlers.Commands;
using Warden.Services.Storage.Rethink;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;

namespace Warden.Services.Storage
{
    public class Program
    {
        private static IConfiguration _configuration;

        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.Storage";
            var config = new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/vhost",
                Hostnames = {"production"}
            };
            var env = "dev";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            var settings = GetConfigurationValue<RethinkDbSettings>("rethinkDb");
            var storage = new RethinkDbWardenCheckStorage(settings);
            var client = BusClientFactory.CreateDefault();
            client.SubscribeAsync<ProcessWardenCheckResult>(async (msg, context) =>
                    new ProcessWardenCheckResultHandler(client, storage).HandleAsync(msg));
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static T GetConfigurationValue<T>(string section) where T : new()
        {
            var configurationValue = new T();
            _configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}
