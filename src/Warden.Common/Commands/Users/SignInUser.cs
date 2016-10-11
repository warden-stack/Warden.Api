using System;

namespace Warden.Common.Commands.Users
{
    public class SignInUser : IAuthenticatedCommand
    {
        public CommandDetails Details { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
    }
}