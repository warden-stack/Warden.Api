using System.Threading.Tasks;
using NLog;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;
using Warden.Common.Events.Users;
using Warden.Services.Users.Auth0;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Handlers
{
    public class SignInUserHandler : ICommandHandler<SignInUser>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;
        private readonly IAuth0RestClient _auth0RestClient;
        private readonly IBusClient _bus;

        public SignInUserHandler(IUserService userService,
            IAuth0RestClient auth0RestClient,
            IBusClient bus)
        {
            _userService = userService;
            _auth0RestClient = auth0RestClient;
            _bus = bus;
        }

        public async Task HandleAsync(SignInUser command)
        {
            Logger.Info("Received SignIn command.");
            var auth0User = await _auth0RestClient.GetUserByAccessTokenAsync(command.AccessToken);
            var user = await _userService.GetAsync(auth0User.UserId);
            if (user.HasNoValue)
            {
                await _userService.CreateAsync(auth0User.Email, auth0User.UserId);
                Logger.Info("Publishing event UserCreated.");
                await _bus.PublishAsync(new UserCreated(auth0User.Email, auth0User.UserId));
            }
            Logger.Info("Publishing event UserSignedIn.");
            await _bus.PublishAsync(new UserSignedIn(auth0User.UserId));
        }
    }
}