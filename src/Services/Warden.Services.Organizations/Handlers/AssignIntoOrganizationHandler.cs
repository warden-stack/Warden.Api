using System.Threading.Tasks;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class AssignIntoOrganizationHandler : ICommandHandler<AssignIntoOrganization>
    {
        private readonly IOrganizationService _organizationServixce;

        public AssignIntoOrganizationHandler(IOrganizationService organizationServixce)
        {
            _organizationServixce = organizationServixce;
        }

        public async Task HandleAsync(AssignIntoOrganization command)
        {
            await _organizationServixce.AssignUserAsync(command.OrganizationId,
                command.UserId, command.Email, command.Role);
        }
    }
}