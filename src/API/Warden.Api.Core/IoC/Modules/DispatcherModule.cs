using System.Reflection;
using Autofac;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Events;
using Warden.Common.Commands;
using Warden.Common.Events;
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

            var coreAssembly = Assembly.Load(new AssemblyName("Warden.Api.Core"));
            var infrastructureAssembly = Assembly.Load(new AssemblyName("Warden.Api.Infrastructure"));
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(infrastructureAssembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterAssemblyTypes(infrastructureAssembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}