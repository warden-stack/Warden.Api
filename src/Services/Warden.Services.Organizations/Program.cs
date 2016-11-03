using Warden.Common.Commands.Organizations;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.Users;
using Warden.Common.Host;
using Warden.Services.Organizations.Framework;

namespace Warden.Services.Organizations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10002)
                .UseAutofac(Bootstrapper.LifetimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToCommand<CreateOrganization>()
                .SubscribeToCommand<CreateWarden>()
                .SubscribeToCommand<DeleteWarden>()
                .SubscribeToEvent<NewUserSignedIn>()
                .SubscribeToEvent<UserSignedIn>()
                .Build()
                .Run();
        }
    }
}
