using System;

namespace Warden.Common.Commands.Wardens
{
    public class CreateWarden : IAuthenticatedCommand
    {
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}