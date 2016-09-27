namespace Warden.Common.Commands.Organizations
{
    public class RequestCreateOrganization : IFeatureRequestCommand
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}