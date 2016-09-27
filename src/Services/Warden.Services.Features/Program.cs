using Warden.Services.Features.Framework;
using Warden.Services.Host;

namespace Warden.Services.Features
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10007)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .Build()
                .Run();
        }
    }
}
