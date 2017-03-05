using Warden.Api.Framework;
using Warden.Common.Host;
using Warden.Messages.Events.Users;

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
