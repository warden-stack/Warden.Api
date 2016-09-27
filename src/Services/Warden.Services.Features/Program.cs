using Warden.Common.Commands.ApiKeys;
using Warden.Common.Events.ApiKeys;
using Warden.Common.Events.Users;
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
                .SubscribeToEvent<ApiKeyCreated>()
                .SubscribeToEvent<UserCreated>()
                .Build()
                .Run();
        }
    }
}
