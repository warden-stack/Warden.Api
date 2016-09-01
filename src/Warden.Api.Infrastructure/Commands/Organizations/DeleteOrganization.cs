using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Organizations
{
    public class DeleteOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid Id { get; set; }
    }

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