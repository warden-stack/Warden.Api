using System;

namespace Warden.Common.Events.Operations
{
    public class OperationUpdated : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid RequestId { get; }
        public string UserId { get; }
        public string State { get; }
        public DateTime UpdatedAt { get; }
        public DateTime? CompletedAt { get; }

        protected OperationUpdated()
        {
        }

        public OperationUpdated(Guid commandId, Guid requestId,
            string userId, string state,
            DateTime updatedAt, DateTime? completedAt)
        {
            CommandId = commandId;
            RequestId = requestId;
            UserId = userId;
            State = state;
            UpdatedAt = updatedAt;
            CompletedAt = completedAt;
        }
    }
}