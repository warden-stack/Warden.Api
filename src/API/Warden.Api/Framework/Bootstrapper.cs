using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Api.Core.IoC.Modules;
using Warden.Api.Core.Settings;
using Warden.Api.Core.Storage;

namespace Warden.Api.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        private readonly IContainer _existingContainer;

        public Bootstrapper(IConfiguration configuration, IContainer existingContainer)
        {
            _configuration = configuration;
            _existingContainer = existingContainer;
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
                builder.RegisterModule<DispatcherModule>();
                builder.RegisterModule<StorageModule>();
                builder.RegisterInstance(new MemoryCache(new MemoryCacheOptions())).As<IMemoryCache>().SingleInstance();
                foreach (var component in _existingContainer.ComponentRegistry.Registrations)
                {
                    builder.RegisterComponent(component);
                }
            });
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                AddCorsHeaders(ctx.Response);

                return ctx.Response;
            });
            pipelines.AfterRequest += (ctx) =>
            {
                AddCorsHeaders(ctx.Response);
            };
            Logger.Info("API Started");
        }

        private static void AddCorsHeaders(Response response)
        {
            response?.WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE")
                .WithHeader("Access-Control-Allow-Headers",
                    "Authorization,Accept,Origin,Content-Type,User-Agent,X-Requested-With")
                .WithHeader("Access-Control-Expose-Headers", "X-ResourceId");
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