using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Organizations;
using Warden.Services.WardenChecks.Domain;
using Warden.Services.WardenChecks.Repositories;

namespace Warden.Services.WardenChecks.Handlers
{
    public class OrganizationCreatedHandler : IEventHandler<OrganizationCreated>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationCreatedHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task HandleAsync(OrganizationCreated @event)
        {
            var organization = await _organizationRepository.GetAsync(@event.UserId, @event.Name);
            if (organization.HasValue)
                return;

            await _organizationRepository.AddAsync(new Organization(@event.OrganizationId, 
                @event.Name, @event.UserId));
        }
    }
}