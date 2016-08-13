using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Organizations
{
    public class AddUserToOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public class AddUserToOrganizationHandler : ICommandHandler<AddUserToOrganization>
    {
        private readonly IOrganizationService _organizationServixce;

        public AddUserToOrganizationHandler(IOrganizationService organizationServixce)
        {
            _organizationServixce = organizationServixce;
        }

        public async Task HandleAsync(AddUserToOrganization command)
        {
            await _organizationServixce.AddUserAsync(command.Id, command.Email);
        }
    }
}