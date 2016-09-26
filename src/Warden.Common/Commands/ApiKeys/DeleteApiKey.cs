using System;

namespace Warden.Common.Commands.ApiKeys
{
    public class DeleteApiKey : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public string Key { get; set; }
    }
}