using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Common.Commands;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Wardens;
using Warden.Common.Extensions;
using Warden.Common.Mongo;
using Warden.Common.Nancy;
using Warden.Services.WardenChecks.Handlers;
using Warden.Services.WardenChecks.Repositories;
using Warden.Services.WardenChecks.Services;

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
                builder.RegisterInstance(_configuration.GetSettings<MongoDbSettings>());
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
                builder.RegisterType<WardenCheckResultRootMinifiedRepository>()
                    .As<IWardenCheckResultRootMinifiedRepository>();
                builder.RegisterType<WardenService>().As<IWardenService>();
                builder.RegisterType<WardenCheckStorage>().As<IWardenCheckStorage>();
                builder.RegisterType<WardenCheckService>().As<IWardenCheckService>();
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterType<ProcessWardenCheckResultHandler>()
                    .As<ICommandHandler<ProcessWardenCheckResult>>();
                builder.RegisterType<OrganizationCreatedHandler>()
                    .As<IEventHandler<OrganizationCreated>>();
                builder.RegisterType<WardenCreatedHandler>()
                    .As<IEventHandler<WardenCreated>>();
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