using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Warden.Api.Infrastructure.IoC.Modules;

namespace Warden.Api.Infrastructure.IoC
{
    public static class Container
    {
        public static IContainer Resolve(IEnumerable<ServiceDescriptor> services, string database)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DispatcherModule>();
            builder.RegisterModule<MapperModule>();
            builder.RegisterModule<ServiceModule>();
            switch (database.ToLowerInvariant())
            {
                case "mongo":
                    builder.RegisterModule<MongoModule>();
                    break;
                default:
                    throw new ArgumentException($"Invalid database: {database}", nameof(database));
            }

            return builder.Build();
        }
    }
}