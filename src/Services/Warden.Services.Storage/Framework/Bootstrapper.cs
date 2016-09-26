﻿using Autofac;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.vNext;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Services.Extensions;
using Warden.Services.Nancy;
using Warden.Services.Storage.Handlers.Commands;
using Warden.Services.Storage.Rethink;

namespace Warden.Services.Storage.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private readonly IConfiguration _configuration;
        public static ILifetimeScope LifetimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            container.Update(builder =>
            {
                builder.Register(x => _configuration.GetSettings<RethinkDbSettings>()).As<RethinkDbSettings>();
                builder.RegisterType<RethinkDbWardenCheckStorage>().As<IWardenCheckStorage>();
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterType<ProcessWardenCheckResultHandler>().As<ICommandHandler<ProcessWardenCheckResult>>();
            });
            LifetimeScope = container;
        }
    }
}