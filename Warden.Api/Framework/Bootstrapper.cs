using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using Warden.Common.Security;
using Warden.Api.IoC;
using Warden.Api.Settings;
using Warden.Common.Extensions;
using Warden.Common.Caching.Redis;
using Warden.Common.Tasks;
using Warden.Common.Nancy;
using Warden.Common.Nancy.Serialization;
using Newtonsoft.Json;
using Warden.Common.RabbitMq;
using Warden.Common.Handlers;
using Warden.Common.Exceptionless;

namespace Warden.Api.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static IExceptionHandler _exceptionHandler;
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
                builder.RegisterInstance(_configuration.GetSettings<RedisSettings>());
                builder.RegisterInstance(_configuration.GetSettings<StorageSettings>());
                builder.RegisterInstance(_configuration.GetSettings<AppSettings>()).SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<FeatureSettings>()).SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<JwtTokenSettings>()).SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<ExceptionlessSettings>()).SingleInstance();
                builder.RegisterType<ExceptionlessExceptionHandler>().As<IExceptionHandler>().SingleInstance();
                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                builder.RegisterModule<ModuleContainer>();
                builder.RegisterModule(new TasksModule(typeof(Startup).GetTypeInfo().Assembly));
                foreach (var component in _existingContainer.ComponentRegistry.Registrations)
                {
                    builder.RegisterComponent(component);
                }
                SecurityContainer.Register(builder, _configuration);
                RabbitMqContainer.Register(builder, _configuration.GetSettings<RawRabbitConfiguration>());
            });
            LifetimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                _exceptionHandler.Handle(ex, ctx.ToExceptionData(),
                    "Request details", "Warden", "API");
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
            pipelines.SetupTokenAuthentication(container);
            _exceptionHandler = container.Resolve<IExceptionHandler>();
            var tasks = container.Resolve<IEnumerable<ITask>>();
            var tasksHandler = container.Resolve<ITaskHandler>();
            Task.Factory.StartNew(async () => 
                { 
                    //Wait till Storage Service is ready.
                    await Task.Delay(5000);
                    await tasksHandler.ExecuteTasksAsync(tasks); 
                }, TaskCreationOptions.LongRunning);

            Logger.Info("Warden API has started.");
        }

        private static void AddCorsHeaders(Response response)
        {
            response?.WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE")
                .WithHeader("Access-Control-Allow-Headers",
                    "Authorization,Accept,Origin,Content-Type,User-Agent,X-Requested-With")
                .WithHeader("Access-Control-Expose-Headers", "X-ResourceId,X-Resource,X-Operation");
        }
    }
}