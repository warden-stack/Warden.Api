using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Organizations;
using Warden.Common.DTO.Users;
using Warden.Common.Events;
using Warden.Common.Events.Organizations;
using Warden.Common.Events.Users;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
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

            await _organizationRepository.AddAsync(new OrganizationDto
            {
                Id = Guid.NewGuid(),
                Name = @event.Name,
                OwnerId = @event.UserId
            });
        }
    }
}