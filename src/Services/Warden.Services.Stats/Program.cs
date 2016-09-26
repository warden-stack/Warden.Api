using Warden.Services.Events;
using Warden.Services.Host;
using Warden.Services.Stats.Framework;

namespace Warden.Services.Stats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10005)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .Build()
                .Run();
        }
    }
}
