using System;
using System.Threading.Tasks;

namespace Warden.Common.Commands.Users
{
    public class UnassignFromOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
    }

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
                command.Email, command.AuthenticatedUserId);
        }
    }
}