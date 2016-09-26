using System.Threading.Tasks;
using Warden.Api.Core.Auth0;
using Warden.Api.Core.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;

namespace Warden.Api.Core.Commands.Handlers
{
    public class RegisterUserHandler : ICommandHandler<SignInUser>
    {
        private readonly IUserService _userService;
        private readonly IAuth0RestClient _auth0RestClient;

        public RegisterUserHandler(IUserService userService,
            IAuth0RestClient auth0RestClient)
        {
            _userService = userService;
            _auth0RestClient = auth0RestClient;
        }

        public async Task HandleAsync(SignInUser command)
        {
            var user = await _auth0RestClient.GetUserByAccessTokenAsync(command.AccessToken);
            await _userService.SignInUserAsync(user.Email, user.UserId, user.Picture);
        }
    }
}