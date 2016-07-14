using System;
using System.Linq;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Core.Domain
{
    public class User : IdentifiableEntity, ITimestampable
    {
        public string Email { get; protected set; }
        public Role Role { get; protected set; }
        public State State { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public Guid RecentlyViewedOrganizationId { get; protected set; }
        public Guid RecentlyViewedWardenId { get; protected set; }

        protected User()
        {
        }

        public User(string email, Role role = Role.User)
        {
            SetEmail(email);
            Role = role;
            State = State.Inactive;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
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

        public void SetRecentlyViewedWardenInOrganization(Organization organization, Guid wardenId)
        {
            var organizationId = organization?.Id ?? Guid.Empty;
            var foundWardenId = organization?.Wardens.Any(x => x.Id == wardenId) == true ? wardenId : Guid.Empty;
            if (RecentlyViewedOrganizationId == organizationId && RecentlyViewedWardenId == foundWardenId)
                return;

            RecentlyViewedOrganizationId = organizationId;
            RecentlyViewedWardenId = foundWardenId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}