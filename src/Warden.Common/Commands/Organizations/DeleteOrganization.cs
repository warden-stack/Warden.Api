using System;

namespace Warden.Common.Commands.Organizations
{
    public class DeleteOrganization : IAuthenticatedCommand
    {
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
        public Guid Id { get; set; }
    }
}