using System;

namespace Warden.Common.Commands.Organizations
{
    public class DeleteOrganization : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public Guid Id { get; set; }
    }
}