namespace Warden.Common.Commands.Organizations
{
    public class CreateOrganization : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}