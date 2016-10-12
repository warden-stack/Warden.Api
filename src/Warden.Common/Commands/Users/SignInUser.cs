namespace Warden.Common.Commands.Users
{
    public class SignInUser : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
    }
}