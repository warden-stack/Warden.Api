using Warden.Services.Commands;
using Warden.Services.Host;
using Warden.Services.RealTime.Framework;

namespace Warden.Services.RealTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 5003)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .SubscribeToCommand<ProcessWardenCheckResult>()
                .Build()
                .Run();
        }
    }
}
