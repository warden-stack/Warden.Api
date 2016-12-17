using Warden.Api.Framework;
using Warden.Common.Host;
using Warden.Services.Users.Shared.Events;

namespace Warden.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(name: "Warden API", port: 5000, integrateWithIIS: true)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToEvent<ApiKeyCreated>()
                .Build()
                .Run();
        }
    }
}
