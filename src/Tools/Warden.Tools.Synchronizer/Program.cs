using Warden.Common.Host;
using Warden.Tools.Synchronizer.Framework;

namespace Warden.Tools.Synchronizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10100)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .Build()
                .Run();
        }
    }
}
