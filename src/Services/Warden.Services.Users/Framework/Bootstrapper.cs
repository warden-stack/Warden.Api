using Autofac;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using NLog;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Common.Commands;
using Warden.Common.Commands.ApiKeys;
using Warden.Common.Commands.Users;
using Warden.Services.Encryption;
using Warden.Services.Extensions;
using Warden.Services.Mongo;
using Warden.Services.Nancy;
using Warden.Services.Users.Auth0;
using Warden.Services.Users.Handlers;
using Warden.Services.Users.Repositories;
using Warden.Services.Users.Services;
using Warden.Services.Users.Settings;

namespace Warden.Services.Users.Framework
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
                builder.RegisterInstance(_configuration.GetSettings<Auth0Settings>());
                builder.RegisterInstance(_configuration.GetSettings<FeatureSettings>());
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterType<Encrypter>().As<IEncrypter>();
                builder.RegisterType<Auth0RestClient>().As<IAuth0RestClient>();
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterType<CreateApiKeyHandler>().As<ICommandHandler<CreateApiKey>>();
                builder.RegisterType<SignInUserHandler>().As<ICommandHandler<SignInUser>>();
                builder.RegisterType<UserRepository>().As<IUserRepository>();
                builder.RegisterType<ApiKeyRepository>().As<IApiKeyRepository>();
                builder.RegisterType<UserService>().As<IUserService>();
                builder.RegisterType<ApiKeyService>().As<IApiKeyService>();
                builder.RegisterType<DatabaseSeeder>().As<IDatabaseSeeder>();
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
            Logger.Info("Warden.Services.Users API Started");
        }
    }
}