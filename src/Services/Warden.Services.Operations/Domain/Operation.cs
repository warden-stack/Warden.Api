using System;
using Warden.Common.Domain;
using Warden.Common.Extensions;

namespace Warden.Services.Operations.Domain
{
    public class Operation : IdentifiableEntity, ITimestampable
    {
        public Guid RequestId { get; protected set; }
        public string UserId { get; protected set; }
        public string Origin { get; protected set; }
        public string Resource { get; protected set; }
        public string State { get; protected set; }
        public string Message { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Operation()
        {
        }

        public Operation(Guid requestId, string userId,
            string origin, string resource, DateTime createdAt,
            string message = "")
        {
            RequestId = requestId;
            UserId = userId;
            Origin = origin;
            Resource = resource;
            CreatedAt = createdAt;
            State = States.Accepted;
            UpdatedAt = DateTime.UtcNow;
            SetMessage(message);
        }

        public void Complete()
        {
            if (State.EqualsCaseInvariant(States.Rejected))
                throw new InvalidOperationException($"Operation: {Id} has been rejected and can not be completed.");

            SetState(States.Completed);
        }

        public void Reject()
        {
            if (State.EqualsCaseInvariant(States.Completed))
                throw new InvalidOperationException($"Operation: {Id} has been completed and can not be rejected.");

            SetState(States.Rejected);
        }

        public void Process()
        {
            if (State.EqualsCaseInvariant(States.Completed))
                throw new InvalidOperationException($"Operation: {Id} has been completed and can not be processed.");
            if (State.EqualsCaseInvariant(States.Rejected))
                throw new InvalidOperationException($"Operation: {Id} has been rejected and can not be processed.");

            SetState(States.Processing);
        }

        public void SetMessage(string message)
        {
            if (message?.Length > 500)
            {
                throw new ArgumentException("Message can not have more than 500 characters.",
                    nameof(message));
            }

            Message = message;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetState(string state)
        {
            if(State.EqualsCaseInvariant(state))
                return;

            State = state;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}