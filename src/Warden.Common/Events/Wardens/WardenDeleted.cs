using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenDeleted : IEvent
    {
        public Guid WardenId { get; set; }
        public Guid OrganizationId { get; set; }

        public WardenDeleted(Guid wardenId, Guid organizationId)
        {
            WardenId = wardenId;
            OrganizationId = organizationId;
        }
    }
}