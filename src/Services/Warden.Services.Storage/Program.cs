using Warden.Common.Events.ApiKeys;
using Warden.Common.Events.Features;
using Warden.Common.Events.Operations;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Users;
using Warden.Common.Events.Wardens;
using Warden.Common.Host;
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
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToEvent<ApiKeyCreated>()
                .SubscribeToEvent<NewUserSignedIn>()
                .SubscribeToEvent<UserSignedIn>()
                .SubscribeToEvent<UserPaymentPlanCreated>()
                .SubscribeToEvent<OrganizationCreated>()
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .SubscribeToEvent<WardenCreated>()
                .SubscribeToEvent<OperationCreated>()
                .SubscribeToEvent<OperationUpdated>()
                .Build()
                .Run();
        }
    }
}
