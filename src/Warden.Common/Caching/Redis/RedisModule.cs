using Autofac;
using StackExchange.Redis;

namespace Warden.Common.Caching.Redis
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

            builder.RegisterType<RedisCache>()
                .As<ICache>()
                .SingleInstance();
        }
    }
}