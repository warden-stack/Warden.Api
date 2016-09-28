using Warden.Common.Commands.Organizations;
using Warden.Common.Events.Users;
using Warden.Services.Host;
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
                .UseRabbitMq()
                .SubscribeToCommand<CreateOrganization>()
                .SubscribeToEvent<NewUserSignedIn>()
                .SubscribeToEvent<UserSignedIn>()
                .Build()
                .Run();
        }
    }
}
