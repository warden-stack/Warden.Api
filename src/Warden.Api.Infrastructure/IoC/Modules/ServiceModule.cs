using Autofac;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WardenService>()
                .As<IWardenService>()
                .InstancePerLifetimeScope();
        }
    }
}