using System.Threading.Tasks;

namespace Warden.Common.Commands.Users
{
    public class SignInUser : ICommand
    {
        public string AccessToken { get; set; }
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
            var user = await _auth0RestClient.GetUserByAccessTokenAsync(command.AccessToken);
            await _userService.SignInUserAsync(user.Email, user.UserId, user.Picture);
        }
    }
}