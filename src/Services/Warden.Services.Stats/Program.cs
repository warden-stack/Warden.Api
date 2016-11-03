using Warden.Common.Events.Wardens;
using Warden.Common.Host;
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
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .Build()
                .Run();
        }
    }
}
