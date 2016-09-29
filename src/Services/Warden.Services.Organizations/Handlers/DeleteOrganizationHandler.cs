using System.Threading.Tasks;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class DeleteOrganizationHandler : ICommandHandler<DeleteOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public DeleteOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(DeleteOrganization command)
        {
            await _organizationService.DeleteAsync(command.Id, command.UserId);
        }
    }
}