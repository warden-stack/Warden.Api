using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Events;
using Warden.Api.Infrastructure.IoC.Modules;

namespace Warden.Api.Infrastructure.IoC
{
    public static class Container
    {
        public static IContainer Resolve(IEnumerable<ServiceDescriptor> services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DispatcherModule>();

            return builder.Build();
        }
    }
}