using Warden.Common.Events.Wardens;
using Warden.Common.Host;
using Warden.Services.RealTime.Framework;

namespace Warden.Services.RealTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10004)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .Build()
                .Run();
        }
    }
}
