using Warden.Common.Commands;
using Warden.Services.Host;
using Warden.Services.Storage.Framework;

namespace Warden.Services.Storage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10000)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                //.SubscribeToEvent<>
                .Build()
                .Run();
        }
    }
}
