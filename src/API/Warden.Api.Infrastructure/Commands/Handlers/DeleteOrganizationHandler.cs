using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;

namespace Warden.Api.Infrastructure.Commands.Handlers
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
            await _organizationService.DeleteAsync(command.Id, command.AuthenticatedUserId);
        }
    }
}