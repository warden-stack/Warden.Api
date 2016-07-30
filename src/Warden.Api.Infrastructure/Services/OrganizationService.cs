using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Events.Organizations;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Events;

namespace Warden.Api.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserService _userService;
        private readonly IEventDispatcher _eventDispatcher;

        public OrganizationService(IOrganizationRepository organizationRepository, 
            IUserService userService, 
            IEventDispatcher eventDispatcher)
        {
            _organizationRepository = organizationRepository;
            _userService = userService;
            _eventDispatcher = eventDispatcher;
        }

        public async Task CreateAsync(Guid userId, string name)
        {
            var user = await _userService.GetAsync(userId);
            var organization = new Organization(name, user);
            await _organizationRepository.AddAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationCreated(organization.Id));
        }
    }
}