using Autofac;
using System.Reflection;
using Warden.Common.Events;
using Module = Autofac.Module;

namespace Warden.Services.Storage.Framework
{
    public class EventHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var coreAssembly = typeof(Startup).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}