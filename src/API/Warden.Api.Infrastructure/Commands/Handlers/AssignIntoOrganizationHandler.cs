using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;

namespace Warden.Api.Infrastructure.Commands.Handlers
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
            await _organizationServixce.AssignUserAsync(command.OrganizationId, command.Email, command.AuthenticatedUserId);
        }
    }
}