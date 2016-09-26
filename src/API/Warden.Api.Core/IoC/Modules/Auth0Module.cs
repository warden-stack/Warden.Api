using Autofac;
using Warden.Api.Core.Auth0;

namespace Warden.Api.Core.IoC.Modules
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