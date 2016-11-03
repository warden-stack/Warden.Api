using System;
using Warden.Common.Domain;
using Warden.Common.Extensions;

namespace Warden.Services.Features.Domain
{
    public class User : IdentifiableEntity, ITimestampable
    {
        public string UserId { get; set; }
        public string Email { get; protected set; }
        public string Role { get; protected set; }
        public string State { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public Guid? PaymentPlanId { get; protected set; }
        
        protected User()
        {
        }

        public User(string email, string userId, string role, string state)
        {
            SetEmail(email);
            SetUserId(userId);
            SetRole(role);
            SetState(state);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (email.Empty())
            {
                Email = string.Empty;
                UpdatedAt = DateTime.UtcNow;

                return;

            }
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

        public void SetRole(string role)
        {
            if (Role == role)
                return;

            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetState(string state)
        {
            if (State == state)
                return;

            State = state;
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