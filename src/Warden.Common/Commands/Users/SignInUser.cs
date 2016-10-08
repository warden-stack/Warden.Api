namespace Warden.Common.Commands.Users
{
    public class SignInUser : IAuthenticatedCommand
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
    }
}