using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events;
using Warden.Common.Events.Users;
using Warden.Common.Extensions;
using Warden.Services.Mongo;
using Warden.Services.Nancy;
using Warden.Services.Organizations.Handlers;
using Warden.Services.Organizations.Repositories;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Framework
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
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterType<UserRepository>().As<IUserRepository>();
                builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
                builder.RegisterType<WardenService>().As<IWardenService>();
                builder.RegisterType<OrganizationService>().As<IOrganizationService>();
                builder.RegisterType<CreateOrganizationHandler>().As<ICommandHandler<CreateOrganization>>();
                builder.RegisterType<CreateWardenHandler>().As<ICommandHandler<CreateWarden>>();
                builder.RegisterType<NewUserSignedInHandler>().As<IEventHandler<NewUserSignedIn>>();
                builder.RegisterType<UserSignedInHandler>().As<IEventHandler<UserSignedIn>>();
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
            Logger.Info("Warden.Services.Organizations API Started");
        }
    }
}