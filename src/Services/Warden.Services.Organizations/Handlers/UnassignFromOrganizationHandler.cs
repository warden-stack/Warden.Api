using System.Threading.Tasks;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class UnassignFromOrganizationHandler : ICommandHandler<UnassignFromOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public UnassignFromOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(UnassignFromOrganization command)
        {
            await _organizationService.UnassignUserAsync(command.OrganizationId, command.UserId);
        }
    }
}