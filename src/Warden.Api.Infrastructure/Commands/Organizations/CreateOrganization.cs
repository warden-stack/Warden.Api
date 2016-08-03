using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Organizations
{
    public class CreateOrganization : IAuthenticatedCommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public string Name { get; set; }
    }

    public class CreateOrganizationHandler : ICommandHandler<CreateOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public CreateOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(CreateOrganization command)
        {
            await _organizationService.CreateAsync(command.AuthenticatedUserId, command.Name);
        }
    }
}