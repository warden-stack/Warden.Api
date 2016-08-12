using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Core.Events.Organizations;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.DTO.Organizations;
using Warden.Api.Infrastructure.Events;

namespace Warden.Api.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public OrganizationService(IMapper mapper,
            IOrganizationRepository organizationRepository, 
            IUserRepository userRepository,
            IEventDispatcher eventDispatcher)
        {
            _mapper = mapper;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<PagedResult<OrganizationDto>> BrowseAsync(Guid userId)
        {
            var organizationValues = await _organizationRepository.BrowseAsync(userId, Guid.Empty);
            var organizationDtos = _mapper.Map<IEnumerable<OrganizationDto>>(organizationValues.Items);
            var organizations = PagedResult<OrganizationDto>.From(organizationValues, organizationDtos);

            return organizations;
        }

        public async Task<OrganizationDto> GetAsync(Guid id)
        {
            var organizationValue = await _organizationRepository.GetAsync(id);
            if (organizationValue.HasNoValue)
                throw new ServiceException($"Organization with id: {id} does not exist.");

            var organization = _mapper.Map<OrganizationDto>(organizationValue.Value);

            return organization;
        }

        public async Task UpdateAsync(Guid id, string name)
        {
            var organizationValue = await _organizationRepository.GetAsync(id);
            if (organizationValue.HasNoValue)
                throw new ServiceException($"Desired organization does not exist, id: {id}");
            var organization = organizationValue.Value;
            organization.SetName(name);
            await _organizationRepository.UpdateAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationUpdated(organization.Id));
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