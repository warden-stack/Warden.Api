using System;

namespace Warden.Common.Events.Operations
{
    public class OperationCreated : IAuthenticatedEvent
    {
        public Guid RequestId { get; }
        public string UserId { get; }
        public string Origin { get; }
        public string Resource { get; }
        public string State { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public string Message { get; }

        protected OperationCreated()
        {
        }

        public OperationCreated(Guid requestId, 
            string userId, string origin, string resource, 
            string state, DateTime createdAt, DateTime updatedAt,
            string message)
        {
            RequestId = requestId;
            UserId = userId;
            Origin = origin;
            Resource = resource;
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Message = message;
        }
    }
}