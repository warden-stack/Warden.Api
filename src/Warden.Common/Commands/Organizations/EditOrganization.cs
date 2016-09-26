using System;
using System.Threading.Tasks;

namespace Warden.Common.Commands.Organizations
{
    public class EditOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class EditOrganizationHandler : ICommandHandler<EditOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public EditOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(EditOrganization command)
        {
            await _organizationService.UpdateAsync(command.Id, command.Name, command.AuthenticatedUserId);
        }
    }
}