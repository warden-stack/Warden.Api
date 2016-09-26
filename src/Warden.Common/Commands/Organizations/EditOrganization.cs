using System;

namespace Warden.Common.Commands.Organizations
{
    public class EditOrganization : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}