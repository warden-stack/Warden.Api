using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Users
{
    public class AssignIntoOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Email { get; set; }
    }

    public class AssignIntoOrganizationHandler : ICommandHandler<AssignIntoOrganization>
    {
        private readonly IOrganizationService _organizationServixce;

        public AssignIntoOrganizationHandler(IOrganizationService organizationServixce)
        {
            _organizationServixce = organizationServixce;
        }

        public async Task HandleAsync(AssignIntoOrganization command)
        {
            await _organizationServixce.AssignUserAsync(command.OrganizationId, command.Email, command.AuthenticatedUserId);
        }
    }
}