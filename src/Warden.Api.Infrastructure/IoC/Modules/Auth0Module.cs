using Autofac;
using Warden.Api.Infrastructure.Auth0;

namespace Warden.Api.Infrastructure.IoC.Modules
{
    public class Auth0Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Auth0RestClient>()
                .As<IAuth0RestClient>()
                .InstancePerLifetimeScope();
        }
    }
}