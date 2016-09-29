using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Organizations;
using Warden.DTO.Organizations;
using Warden.DTO.Wardens;
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
                Id = @event.OrganizationId,
                Name = @event.Name,
                Description = @event.Description,
                OwnerId = @event.UserId,
                Users = new List<UserInOrganizationDto>
                {
                    new UserInOrganizationDto
                    {
                        UserId = @event.UserId,
                        Email = @event.UserEmail,
                        Role = @event.UserOrganizationRole,
                        CreatedAt = @event.UserCreatedAt
                    }
                },
                Wardens = new List<WardenDto>()
            });
        }
    }
}