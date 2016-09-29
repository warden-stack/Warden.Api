using System.Threading.Tasks;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class EditOrganizationHandler : ICommandHandler<EditOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public EditOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(EditOrganization command)
        {
            await _organizationService.UpdateAsync(command.Id, command.Name, command.UserId);
        }
    }
}