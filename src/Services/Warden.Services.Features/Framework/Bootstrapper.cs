using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Common.Caching;
using Warden.Common.Commands;
using Warden.Common.Commands.ApiKeys;
using Warden.Common.Commands.Organizations;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events;
using Warden.Common.Events.ApiKeys;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Users;
using Warden.Common.Events.Wardens;
using Warden.Common.Extensions;
using Warden.Common.Mongo;
using Warden.Common.Nancy;
using Warden.Services.Features.Handlers;
using Warden.Services.Features.Repositories;
using Warden.Services.Features.Services;
using Warden.Services.Features.Settings;

namespace Warden.Services.Features.Framework
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
                builder.RegisterInstance(_configuration.GetSettings<FeatureSettings>());
                builder.RegisterInstance(_configuration.GetSettings<PaymentPlanSettings>());
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterModule<InMemoryCacheModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>();
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterType<UserRepository>().As<IUserRepository>();
                builder.RegisterType<PaymentPlanRepository>().As<IPaymentPlanRepository>();
                builder.RegisterType<UserPaymentPlanRepository>().As<IUserPaymentPlanRepository>();
                builder.RegisterType<WardenChecksCounter>().As<IWardenChecksCounter>();
                builder.RegisterType<UserFeaturesManager>().As<IUserFeaturesManager>();
                builder.RegisterType<UserPaymentPlanService>().As<IUserPaymentPlanService>();
                builder.RegisterType<UserPaymentPlanService>().As<IUserPaymentPlanService>();
                builder.RegisterType<RequestNewApiKeyHandler>().As<ICommandHandler<RequestNewApiKey>>();
                builder.RegisterType<ApiKeyCreatedHandler>().As<IEventHandler<ApiKeyCreated>>();
                builder.RegisterType<RequestWardenCheckResultProcessingResultHandler>()
                    .As<ICommandHandler<RequestWardenCheckResultProcessing>>();
                builder.RegisterType<WardenCheckResultProcessedHandler>()
                    .As<IEventHandler<WardenCheckResultProcessed>>();
                builder.RegisterType<NewUserSignedInHandler>().As<IEventHandler<NewUserSignedIn>>();
                builder.RegisterType<RequestNewWardenHandler>().As<ICommandHandler<RequestNewWarden>>();
                builder.RegisterType<WardenCreatedHandler>().As<IEventHandler<WardenCreated>>();
                builder.RegisterType<RequestNewOrganizationHandler>().As<ICommandHandler<RequestNewOrganization>>();
                builder.RegisterType<OrganizationCreatedHandler>().As<IEventHandler<OrganizationCreated>>();
            });
            LifetimeScope = container;
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var databaseSettings = container.Resolve<MongoDbSettings>();
            var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();
            if (databaseSettings.Seed)
            {
                var seeder = container.Resolve<IDatabaseSeeder>();
                seeder.SeedAsync();
            }
            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            Logger.Info("Warden.Services.Features API Started");
        }
    }
}