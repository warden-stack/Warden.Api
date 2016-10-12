using System;

namespace Warden.Common.Commands.Organizations
{
    public class EditOrganization : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public string UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}