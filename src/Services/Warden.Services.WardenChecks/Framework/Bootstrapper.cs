using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Services.Extensions;
using Warden.Services.Mongo;
using Warden.Services.Nancy;
using Warden.Services.WardenChecks.Handlers;
using Warden.Services.WardenChecks.Rethink;

namespace Warden.Services.WardenChecks.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        public static ILifetimeScope LifetimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            container.Update(builder =>
            {
                builder.RegisterInstance(_configuration.GetSettings<RethinkDbSettings>());
                //builder.RegisterType<RethinkDbWardenCheckStorage>().As<IWardenCheckStorage>();
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterType<ProcessWardenCheckResultHandler>().As<ICommandHandler<ProcessWardenCheckResult>>();
            });
            LifetimeScope = container;
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var databaseSettings = container.Resolve<MongoDbSettings>();
            var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();
            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            Logger.Info("Warden.Services.WardenChecks API Started");
        }
    }
}