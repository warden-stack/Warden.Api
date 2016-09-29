using System;
using Warden.Services.Domain;

namespace Warden.Services.Organizations.Domain
{
    public class UserInOrganization : ITimestampable
    {
        public string UserId { get; protected set; }
        public string Email { get; protected set; }
        public OrganizationRole Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected UserInOrganization()
        {
        }

        protected UserInOrganization(User user, OrganizationRole role)
        {
            if (user == null)
                throw new DomainException("Can not create new user in organization from empty user.");

            UserId = user.UserId;
            Email = user.Email;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public static UserInOrganization Create(User user, OrganizationRole role = OrganizationRole.User)
            => new UserInOrganization(user, role);
    }
}