using System;

namespace Warden.Common.Commands.ApiKeys
{
    public class CreateApiKey : IAuthenticatedCommand
    {
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
    }
}
