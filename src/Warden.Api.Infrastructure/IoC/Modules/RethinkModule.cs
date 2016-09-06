using Autofac;
using Warden.Api.Infrastructure.Rethink;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.IoC.Modules
{
    public class RethinkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RethinkDbManager>()
                .As<IRethinkDbManager>()
                .SingleInstance();

            builder.RegisterType<RealTimeDataStorage>()
                .As<IRealTimeDataStorage>()
                .SingleInstance();

            builder.RegisterType<RealTimeDataPusher>()
                .As<IRealTimeDataPusher>()
                .SingleInstance();
        }
    }
}