using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Warden.Api.IoC.Modules;

namespace Warden.Api.IoC
{
    public static class Container
    {
        public static IContainer Resolve(IEnumerable<ServiceDescriptor> services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DispatcherModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<StorageModule>();
            builder.RegisterModule<FilterModule>();
            builder.RegisterModule<EventHandlersModule>();
            //builder.RegisterModule<RedisModule>();

            return builder.Build();
        }
    }
}