using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;
using Warden.Common.Events.Organizations;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class CreateOrganizationHandler : ICommandHandler<CreateOrganization>
    {
        private readonly IBusClient _bus;
        private readonly IOrganizationService _organizationService;

        public CreateOrganizationHandler(IBusClient bus, 
            IOrganizationService organizationService)
        {
            _bus = bus;
            _organizationService = organizationService;
        }
        
        public async Task HandleAsync(CreateOrganization command)
        {
            await _organizationService.CreateAsync(command.UserId, command.Name, command.Description);
            await _bus.PublishAsync(new OrganizationCreated(command.UserId, command.Name));
        }
    }
}