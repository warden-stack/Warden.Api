using System;
using Warden.Common.Extensions;
using Warden.Services.Domain;

namespace Warden.Services.Users.Domain
{
    public class User : IdentifiableEntity, ITimestampable
    {
        public string ExternalId { get; set; }
        public string Email { get; protected set; }
        public Role Role { get; protected set; }
        public State State { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public Guid RecentlyViewedOrganizationId { get; protected set; }
        public Guid RecentlyViewedWardenId { get; protected set; }
        public Guid? PaymentPlanId { get; protected set; }

        protected User()
        {
        }

        public User(string email, Role role = Role.User, string externalId = null)
        {
            SetEmail(email);
            Role = role;
            State = State.Inactive;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            ExternalId = externalId;
        }

        public void SetEmail(string email)
        {
            if (email.Empty())
                throw new DomainException("Email can not be empty.");

            if (!email.IsEmail())
                throw new DomainException($"Invalid email: '{email}.");

            if (Email.EqualsCaseInvariant(email))
                return;

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetExternalId(string externalId)
        {
            if (externalId.Empty())
                throw new DomainException("ExternalId cannot be empty");

            ExternalId = externalId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(Role role)
        {
            if (Role == role)
                return;

            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Lock()
        {
            if(State == State.Locked)
                return;

            State = State.Locked;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (State == State.Active)
                return;

            State = State.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            if (State == State.Deleted)
                return;

            State = State.Deleted;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}