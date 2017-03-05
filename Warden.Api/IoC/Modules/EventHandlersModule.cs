using System.Reflection;
using Autofac;
using Warden.Messages.Events;
using Module = Autofac.Module;

namespace Warden.Api.IoC.Modules
{
    public class EventHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(EventHandlersModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}