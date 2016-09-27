namespace Warden.Common.Events.Organizations
{
    public class OrganizationCreated : IAuthenticatedEvent
    {
        public string UserId { get; set; }
        public string Name { get; }

        protected OrganizationCreated()
        {
        }

        public OrganizationCreated(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}