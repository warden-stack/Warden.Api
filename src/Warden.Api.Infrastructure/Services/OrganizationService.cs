using System;
using System.Threading.Tasks;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Events.Organizations;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Events;

namespace Warden.Api.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public OrganizationService(IOrganizationRepository organizationRepository, 
            IUserRepository userRepository,
            IEventDispatcher eventDispatcher)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task CreateAsync(Guid userId, string name)
        {
            if (name.Empty())
                throw new ServiceException("Organization name can not be empty.");

            var userValue = await _userRepository.GetAsync(userId);
            if (userValue.HasNoValue)
                throw new ServiceException($"User has not been found for given id: '{userId}'.");

            var organizationValue = await _organizationRepository.GetAsync(name, userId);
            if (organizationValue.HasValue)
            {
                throw new ServiceException($"There's already an organization with name: '{name}' " +
                                           $"for user with id: '{userId}'.");
            }

            var organization = new Organization(name, userValue.Value);
            await _organizationRepository.AddAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationCreated(organization.Id));
        }
    }
}