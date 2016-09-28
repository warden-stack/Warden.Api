using Autofac;
using StackExchange.Redis;
using Warden.Api.Core.Redis;
using Warden.Api.Core.Settings;
using Warden.Api.Core.Storage;

namespace Warden.Api.Core.IoC.Modules
{
    public class RedisModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisDatabaseFactory>()
                .As<IRedisDatabaseFactory>()
                .SingleInstance();

            builder.Register((c, p) =>
                {
                    var settings = c.Resolve<RedisSettings>();
                    var databaseFactory = c.Resolve<IRedisDatabaseFactory>();
                    var database = databaseFactory.GetDatabase(settings.Database);

                    return database;
                }).As<IDatabase>()
                .SingleInstance();

            builder.RegisterType<RedisCache>().As<ICache>().SingleInstance();
        }
    }
}