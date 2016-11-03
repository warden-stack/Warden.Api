using Warden.Common.Commands.ApiKeys;
using Warden.Common.Commands.Users;
using Warden.Common.Events.Features;
using Warden.Common.Host;
using Warden.Services.Users.Framework;

namespace Warden.Services.Users
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10001)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToCommand<CreateApiKey>()
                .SubscribeToCommand<SignInUser>()
                .SubscribeToEvent<UserPaymentPlanCreated>()
                .Build()
                .Run();
        }
    }
}
