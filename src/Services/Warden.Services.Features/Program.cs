using Warden.Common.Commands.ApiKeys;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.ApiKeys;
using Warden.Common.Events.Users;
using Warden.Common.Events.Wardens;
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
                .SubscribeToCommand<RequestNewApiKey>()
                .SubscribeToCommand<RequestCreateWarden>()
                .SubscribeToCommand<RequestProcessWardenCheckResult>()
                .SubscribeToEvent<ApiKeyCreated>()
                .SubscribeToEvent<WardenCheckResultProcessed>()
                .SubscribeToEvent<UserCreated>()
                .SubscribeToEvent<WardenCreated>()
                .Build()
                .Run();
        }
    }
}
