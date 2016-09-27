using System.Threading.Tasks;
using Warden.Api.Core.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;

namespace Warden.Api.Core.Commands.Handlers
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
            await _organizationService.UnassignUserAsync(command.OrganizationId, 
                command.Email, command.UserId);
        }
    }
}