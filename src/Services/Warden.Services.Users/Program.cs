using Warden.Common.Commands.ApiKeys;
using Warden.Common.Commands.Users;
using Warden.Services.Host;
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
                .UseRabbitMq()
                .SubscribeToCommand<CreateApiKey>()
                .SubscribeToCommand<SignInUser>()
                .Build()
                .Run();
        }
    }
}
