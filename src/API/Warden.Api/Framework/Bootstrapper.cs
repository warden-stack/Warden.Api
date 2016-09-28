using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Api.Core.IoC.Modules;
using Warden.Api.Core.Settings;
using Warden.Api.Core.Storage;
using Warden.Services.Nancy;

namespace Warden.Api.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            Logger.Info("Configuring application container");
            base.ConfigureApplicationContainer(container);
            container.Update(builder =>
            {
                builder.RegisterInstance(GetConfigurationValue<Auth0Settings>());
                builder.RegisterInstance(GetConfigurationValue<RedisSettings>());
                builder.RegisterInstance(GetConfigurationValue<StorageSettings>());
                builder.RegisterInstance(BusClientFactory.CreateDefault())
                    .As<IBusClient>();
                builder.RegisterType<StorageClient>()
                    .As<IStorageClient>()
                    .InstancePerLifetimeScope();
                builder.RegisterType<ApiKeyStorage>()
                    .As<IApiKeyStorage>()
                    .InstancePerLifetimeScope();

                builder.RegisterModule<DispatcherModule>();
            });
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };

            Logger.Info("API Started");
        }

        private T GetConfigurationValue<T>(string section = "") where T : new()
        {
            if (string.IsNullOrWhiteSpace(section))
            {
                section = typeof(T).Name.Replace("Settings", string.Empty);
            }

            var configurationValue = new T();
            _configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}