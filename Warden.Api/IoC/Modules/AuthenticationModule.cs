using Autofac;
using Warden.Api.Authentication;

namespace Warden.Api.IoC.Modules
{
    public class AuthenticationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtTokenHandler>()
                .As<IJwtTokenHandler>()
                .SingleInstance();
        }
    }
}