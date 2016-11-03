using Autofac;
using Warden.Api.Services;

namespace Warden.Api.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SecuredRequestService>()
                .As<ISecuredRequestService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UniqueIdGenerator>()
                .As<IUniqueIdGenerator>()
                .SingleInstance();
            builder.RegisterType<IdentityProvider>()
                .As<IIdentityProvider>()
                .SingleInstance();
        }
    }
}