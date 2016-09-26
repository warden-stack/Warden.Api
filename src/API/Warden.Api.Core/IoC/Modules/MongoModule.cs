using Autofac;
using MongoDB.Driver;
using Warden.Api.Core.Mongo;
using Warden.Api.Core.Services;
using Warden.Api.Core.Settings;

namespace Warden.Api.Core.IoC.Modules
{
    public class MongoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
            {
                var settings = c.Resolve<DatabaseSettings>();

                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            builder.Register((c, p) =>
            {
                var mongoClient = c.Resolve<MongoClient>();
                var settings = c.Resolve<DatabaseSettings>();
                var database = mongoClient.GetDatabase(settings.Database);

                return database;
            }).As<IMongoDatabase>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MongoDatabaseSeeder>()
                .As<IDatabaseSeeder>()
                .SingleInstance();

            builder.RegisterType<MongoDatabaseInitializer>()
                .As<IDatabaseInitializer>()
                .SingleInstance();

            builder.RegisterModule<MongoRepositoryModule>();
        }
    }
}