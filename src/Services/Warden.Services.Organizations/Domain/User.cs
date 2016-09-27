using System;
using Warden.Common.DTO.Common;
using Warden.Common.DTO.Users;
using Warden.Common.Extensions;
using Warden.Services.Domain;

namespace Warden.Services.Organizations.Domain
{
    public class User : IdentifiableEntity, ITimestampable
    {
        public string UserId { get; set; }
        public string Email { get; protected set; }
        public Role Role { get; protected set; }
        public State State { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {
        }

        public User(string email, string userId, Role role = Role.User)
        {
            SetEmail(email);
            SetUserId(userId);
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

        public void SetUserId(string userId)
        {
            if (userId.Empty())
                throw new DomainException("User id cannot be empty");

            UserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}