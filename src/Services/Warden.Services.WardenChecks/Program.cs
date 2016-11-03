using Warden.Common.Commands.WardenChecks;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Wardens;
using Warden.Common.Host;
using Warden.Services.WardenChecks.Framework;

namespace Warden.Services.WardenChecks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10003)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToCommand<ProcessWardenCheckResult>()
                .SubscribeToEvent<OrganizationCreated>()
                .SubscribeToEvent<WardenCreated>()
                .Build()
                .Run();
        }
    }
}
