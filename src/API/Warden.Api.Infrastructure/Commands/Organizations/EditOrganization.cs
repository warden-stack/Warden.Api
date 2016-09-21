using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Organizations
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