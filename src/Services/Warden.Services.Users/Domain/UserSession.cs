using System;
using Warden.Common.Domain;

namespace Warden.Services.Users.Domain
{
    public class UserSession : IdentifiableEntity, ITimestampable
    {
        public string UserId { get; protected set; }
        public string UserAgent { get; protected set; }
        public string IpAddress { get; protected set; }
        public string Referrer { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected UserSession()
        {
        }

        public UserSession(User user, string userAgent = null,
            string ipAddress = null, string referrer = null)
        {
            if (user == null)
                throw new DomainException("Can not create an empty user session.");

            UserId = user.UserId;
            UserAgent = userAgent;
            IpAddress = ipAddress;
            Referrer = referrer;
            CreatedAt = DateTime.UtcNow;
        }
    }
}