using Autofac;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Events;
using Module = Autofac.Module;

namespace Warden.Api.Core.IoC.Modules
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