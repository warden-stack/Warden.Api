using System.Reflection;
using Autofac;
using Warden.Common.Events;
using Module = Autofac.Module;

namespace Warden.Api.Core.IoC.Modules
{
    public class EventHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var coreAssembly = typeof(EventHandlersModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}