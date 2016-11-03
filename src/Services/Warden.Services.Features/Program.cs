using Warden.Common.Commands.ApiKeys;
using Warden.Common.Commands.Organizations;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.ApiKeys;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Users;
using Warden.Common.Events.Wardens;
using Warden.Common.Host;
using Warden.Services.Features.Framework;

namespace Warden.Services.Features
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10007)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToCommand<RequestNewApiKey>()
                .SubscribeToCommand<RequestNewWarden>()
                .SubscribeToCommand<RequestWardenCheckResultProcessing>()
                .SubscribeToCommand<RequestNewOrganization>()
                .SubscribeToEvent<ApiKeyCreated>()
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .SubscribeToEvent<NewUserSignedIn>()
                .SubscribeToEvent<WardenCreated>()
                .SubscribeToEvent<OrganizationCreated>()
                .Build()
                .Run();
        }
    }
}
