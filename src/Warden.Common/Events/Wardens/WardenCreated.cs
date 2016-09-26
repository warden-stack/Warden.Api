using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenCreated : IEvent
    {
        public Guid OrganizationId { get; }
        public Guid WardenId { get; }

        public WardenCreated(Guid organizationId, Guid wardenId)
        {
            OrganizationId = organizationId;
            WardenId = wardenId;
        }
    }
}