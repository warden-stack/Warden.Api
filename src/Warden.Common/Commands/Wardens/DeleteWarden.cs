using System;

namespace Warden.Common.Commands.Wardens
{
    public class DeleteWarden : IAuthenticatedCommand
    {
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
        public Guid WardenId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}