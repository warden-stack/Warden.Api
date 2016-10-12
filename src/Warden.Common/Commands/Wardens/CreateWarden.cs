using System;

namespace Warden.Common.Commands.Wardens
{
    public class CreateWarden : IAuthenticatedCommand
    {
        public Request Request { get; set; }
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}