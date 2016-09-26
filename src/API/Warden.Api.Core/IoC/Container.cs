using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Warden.Api.Core.IoC.Modules;
using Autofac.Extensions.DependencyInjection;

namespace Warden.Api.Core.IoC
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
            builder.RegisterModule<Auth0Module>();
            builder.RegisterModule<StorageModule>();
            builder.RegisterModule<RedisModule>();
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