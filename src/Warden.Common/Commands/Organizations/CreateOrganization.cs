namespace Warden.Common.Commands.Organizations
{
    public class CreateOrganization : IAuthenticatedCommand
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}