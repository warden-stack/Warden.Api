using System;

namespace Warden.Common.Commands.Organizations
{
    public class EditOrganization : IAuthenticatedCommand
    {
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}