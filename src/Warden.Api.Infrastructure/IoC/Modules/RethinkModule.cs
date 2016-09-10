using Autofac;
using Warden.Api.Infrastructure.Rethink;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.IoC.Modules
{
    public class RethinkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RethinkDbWardenCheckStorage>()
                .As<IWardenCheckStorage>()
                .SingleInstance();

            builder.RegisterType<RethinkDbRealTimeDataStorage>()
                .As<IRealTimeDataStorage>()
                .SingleInstance();

            builder.RegisterType<RethinkDbRealTimeDataPusher>()
                .As<IRealTimeDataPusher>()
                .SingleInstance();
        }
    }
}