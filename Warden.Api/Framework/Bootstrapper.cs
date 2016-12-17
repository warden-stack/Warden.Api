using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using RawRabbit.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using Warden.Api.IoC.Modules;
using Warden.Api.Settings;
using Warden.Common.Caching;
using Warden.Common.Extensions;
using Warden.Common.Caching.Redis;
using Warden.Common.Tasks;

namespace Warden.Api.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        private readonly IContainer _existingContainer;
        public static ILifetimeScope LifetimeScope { get; private set; }

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
                builder.RegisterInstance(_configuration.GetSettings<Auth0Settings>());
                builder.RegisterInstance(_configuration.GetSettings<RedisSettings>());
                builder.RegisterInstance(_configuration.GetSettings<StorageSettings>());
                var rawRabbitConfiguration = _configuration.GetSettings<RawRabbitConfiguration>();
                builder.RegisterInstance(rawRabbitConfiguration).SingleInstance();
                builder.RegisterInstance(BusClientFactory.CreateDefault(rawRabbitConfiguration))
                    .As<IBusClient>();
                builder.RegisterModule<DispatcherModule>();
                builder.RegisterModule<StorageModule>();
                builder.RegisterModule<FilterModule>();
                builder.RegisterModule<ServiceModule>();
                builder.RegisterModule<EventHandlersModule>();
                builder.RegisterModule<InMemoryCacheModule>();
                builder.RegisterModule(new TasksModule(typeof(Startup).GetTypeInfo().Assembly));
                foreach (var component in _existingContainer.ComponentRegistry.Registrations)
                {
                    builder.RegisterComponent(component);
                }
            });
            LifetimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                ctx.Response = ErrorResponse.FromException(ex, context.Environment);
                AddCorsHeaders(ctx.Response);

                return ctx.Response;
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
            var tasks = container.Resolve<IEnumerable<ITask>>();
            var tasksHandler = container.Resolve<ITaskHandler>();
            Task.Factory.StartNew(() => tasksHandler.ExecuteTasksAsync(tasks), TaskCreationOptions.LongRunning);

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
    }
}