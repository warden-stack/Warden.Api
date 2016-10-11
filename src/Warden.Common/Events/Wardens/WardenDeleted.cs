using System;

namespace Warden.Common.Events.Wardens
{
    public class WardenDeleted : IEvent
    {
        public Guid CommandId { get; }
        public Guid WardenId { get; }
        public Guid OrganizationId { get; }

        protected WardenDeleted()
        {
        }

        public WardenDeleted(Guid commandId, Guid wardenId, Guid organizationId)
        {
            CommandId = commandId;
            WardenId = wardenId;
            OrganizationId = organizationId;
        }
    }
}