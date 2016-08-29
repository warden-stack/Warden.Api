using Autofac;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationService>()
                .As<IOrganizationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WardenService>()
                .As<IWardenService>()
                .InstancePerLifetimeScope();

            //TODO: Register Rebus IBus
            builder.RegisterType<WardenSpawnService>()
                .As<IWardenSpawnService>()
                .InstancePerLifetimeScope();
        }
    }
}