using System.Threading.Tasks;
using Warden.Api.Infrastructure.Auth0;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Users
{
    public class SignInUser : ICommand
    {
        public string ExternalId { get; set; }
    }

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
            var user = await _auth0RestClient.GetUserAsync(command.ExternalId);
            await _userService.SignInUserAsync(user.Email, user.UserId, user.Picture);
        }
    }
}