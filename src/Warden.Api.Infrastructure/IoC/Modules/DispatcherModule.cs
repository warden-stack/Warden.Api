using System.Reflection;
using Autofac;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Events;
using Module = Autofac.Module;

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

            var coreAssembly = Assembly.Load(new AssemblyName("Warden.Api.Core"));
            var infrastructureAssembly = Assembly.Load(new AssemblyName("Warden.Api.Infrastructure"));
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(typeof(IEventHandler<>));
            builder.RegisterAssemblyTypes(infrastructureAssembly).AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterAssemblyTypes(infrastructureAssembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}