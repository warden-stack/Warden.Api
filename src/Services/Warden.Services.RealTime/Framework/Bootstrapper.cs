using Autofac;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.Configuration;
using RawRabbit.vNext;
using RawRabbit.vNext.Disposable;
using Warden.Services.Commands;
using Warden.Services.Nancy;
using Warden.Services.RealTime.Handlers.Commands;
using Warden.Services.RealTime.Hubs;

namespace Warden.Services.RealTime.Framework
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
                builder.RegisterInstance(BusClientFactory.CreateDefault()).As<IBusClient>();
                builder.RegisterInstance(GlobalHost.ConnectionManager.GetHubContext<WardenHub>()).As<IHubContext>();
                builder.RegisterType<SignalRService>().As<ISignalRService>();
                builder.RegisterType<ProcessWardenCheckResultHandler>().As<ICommandHandler<ProcessWardenCheckResult>>();
            });
            LifetimeScope = container;
        }
    }
}