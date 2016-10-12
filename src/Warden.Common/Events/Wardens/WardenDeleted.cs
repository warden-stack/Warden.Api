using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenDeleted : IEvent
    {
        public Guid RequestId { get; }
        public Guid WardenId { get; }
        public Guid OrganizationId { get; }

        protected WardenDeleted()
        {
        }

        public WardenDeleted(Guid requestId, Guid wardenId, Guid organizationId)
        {
            RequestId = requestId;
            WardenId = wardenId;
            OrganizationId = organizationId;
        }
    }
}