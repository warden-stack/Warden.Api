using System;

namespace Warden.Common.Commands.ApiKeys
{
    public class CreateApiKey : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}