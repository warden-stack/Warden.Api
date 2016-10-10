using Warden.Api.Framework;
using Warden.Common.Events.ApiKeys;
using Warden.Common.Host;

namespace Warden.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(name: "Warden API", port: 5000, integrateWithIIS: true)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq()
                .SubscribeToEvent<ApiKeyCreated>()
                .Build()
                .Run();
        }
    }
}
