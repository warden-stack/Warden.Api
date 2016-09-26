using System.Threading.Tasks;
using Warden.Api.Core.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;

namespace Warden.Api.Core.Commands.Handlers
{
    public class CreateOrganizationHandler : ICommandHandler<CreateOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public CreateOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(CreateOrganization command)
        {
            await _organizationService.CreateAsync(command.AuthenticatedUserId, 
                command.Name, command.Description);
        }
    }
}