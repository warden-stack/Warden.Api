namespace Warden.Api.Core.Events.Wardens
{
    public class WardenCreated : IEvent
    {
        public string OrganizationInternalId { get; }
        public string WardenInternalId { get; }

        public WardenCreated(string organizationInternalId, string wardenInternalId)
        {
            OrganizationInternalId = organizationInternalId;
            WardenInternalId = wardenInternalId;
        }
    }
}