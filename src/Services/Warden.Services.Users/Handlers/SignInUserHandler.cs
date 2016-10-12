using System.Threading.Tasks;
using NLog;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;
using Warden.Common.Events.Users;
using Warden.Services.Users.Auth0;
using Warden.Services.Users.Domain;
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
            IApiKeyService apiKeyService,
            IAuth0RestClient auth0RestClient,
            IBusClient bus)
        {
            _userService = userService;
            _auth0RestClient = auth0RestClient;
            _bus = bus;
        }

        public async Task HandleAsync(SignInUser command)
        {
            var auth0User = await _auth0RestClient.GetUserByAccessTokenAsync(command.AccessToken);
            var user = await _userService.GetAsync(auth0User.UserId);
            var userId = string.Empty;
            if (user.HasNoValue)
            {
                await _userService.CreateAsync(auth0User.UserId, auth0User.Email, Roles.User);
                user = await _userService.GetAsync(auth0User.UserId);
                userId = user.Value.UserId;
                await _bus.PublishAsync(new NewUserSignedIn(command.Request.Id, userId, user.Value.Email,
                    user.Value.Role, user.Value.State, user.Value.CreatedAt));

                return;
            }
            userId = user.Value.UserId;
            await _bus.PublishAsync(new UserSignedIn(command.Request.Id,
                userId, user.Value.Email, user.Value.Role, user.Value.State));
        }
    }
}