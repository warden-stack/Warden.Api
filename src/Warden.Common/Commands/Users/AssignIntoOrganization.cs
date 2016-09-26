using System;
using System.Threading.Tasks;

namespace Warden.Common.Commands.Users
{
    public class AssignIntoOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
    }

    public class AssignIntoOrganizationHandler : ICommandHandler<AssignIntoOrganization>
    {
        private readonly IOrganizationService _organizationServixce;

        public AssignIntoOrganizationHandler(IOrganizationService organizationServixce)
        {
            _organizationServixce = organizationServixce;
        }

        public async Task HandleAsync(AssignIntoOrganization command)
        {
            await _organizationServixce.AssignUserAsync(command.OrganizationId, command.Email, command.AuthenticatedUserId);
        }
    }
}