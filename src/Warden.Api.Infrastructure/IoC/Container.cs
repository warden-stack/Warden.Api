using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Warden.Api.Infrastructure.IoC
{
    public static class Container
    {
        public static IContainer Resolve(IEnumerable<ServiceDescriptor> services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            return builder.Build();
        }
    }
}