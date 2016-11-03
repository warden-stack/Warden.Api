using System;
using Warden.Common.Domain;

namespace Warden.Services.Organizations.Domain
{
    public class UserInOrganization : ITimestampable
    {
        public string UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected UserInOrganization()
        {
        }

        protected UserInOrganization(User user, string role)
        {
            if (user == null)
                throw new DomainException("Can not create new user in organization from empty user.");

            UserId = user.UserId;
            Email = user.Email;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public static UserInOrganization Create(User user, string role)
            => new UserInOrganization(user, role);
    }
}