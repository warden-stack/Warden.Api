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
                .SubscribeToCommand<SignInUser>()
                .Build()
                .Run();
        }
    }
}
