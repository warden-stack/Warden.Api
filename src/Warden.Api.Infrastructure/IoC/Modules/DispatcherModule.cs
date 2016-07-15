using Autofac;
using Autofac.Core.Registration;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Events;

namespace Warden.Api.Infrastructure.IoC.Modules
{
    public class DispatcherModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}