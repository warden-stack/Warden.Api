using System;

namespace Warden.Common.Events.Operations
{
    public class OperationCreated : IAuthenticatedEvent
    {
        public Guid CommandId { get; }
        public Guid RequestId { get; }
        public string UserId { get; }
        public string Origin { get; }
        public string Resource { get; }
        public string State { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        protected OperationCreated()
        {
        }

        public OperationCreated(Guid commandId, Guid requestId, 
            string userId, string origin, string resource, 
            string state, DateTime createdAt, DateTime updatedAt)
        {
            CommandId = commandId;
            RequestId = requestId;
            UserId = userId;
            Origin = origin;
            Resource = resource;
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}