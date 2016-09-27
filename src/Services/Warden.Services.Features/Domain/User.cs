using System;
using Warden.Common.DTO.Common;
using Warden.Common.DTO.Users;
using Warden.Common.Extensions;
using Warden.Services.Domain;

namespace Warden.Services.Features.Domain
{
    public class User : IdentifiableEntity, ITimestampable
    {
        public string UserId { get; set; }
        public string Email { get; protected set; }
        public Role Role { get; protected set; }
        public State State { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public Guid? PaymentPlanId { get; protected set; }

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

        public void SetPaymentPlan(UserPaymentPlan userPlan)
        {
            if (userPlan == null)
            {
                PaymentPlanId = null;

                return;
            }

            PaymentPlanId = userPlan.Id;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}