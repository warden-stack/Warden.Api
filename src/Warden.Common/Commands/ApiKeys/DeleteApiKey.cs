using System;

namespace Warden.Common.Commands.ApiKeys
{
    public class DeleteApiKey : IAuthenticatedCommand
    {
        public string UserId { get; set; }
        public string Key { get; set; }
    }
}