using Warden.Common.Host;
using Warden.Services.Operations.Framework;

namespace Warden.Services.Operations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10010)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .Build()
                .Run();
        }
    }
}
