using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;
using Warden.Common.Events.Organizations;
using Warden.Api.Core.Repositories;
using Warden.Common.DTO.Organizations;
using Warden.Api.Infrastructure.Events;
using Warden.Common.Extensions;

namespace Warden.Api.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private const string DefaultName = "My organization";
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

        public async Task<PagedResult<OrganizationDto>> BrowseAsync(string userId)
        {
            var organizationValues = await _organizationRepository.BrowseAsync(userId, string.Empty);
            var organizationDtos = organizationValues.Items.Select(_mapper.Map<OrganizationDto>);
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

        public async Task UpdateAsync(Guid id, string name, string userId)
        {
            var organizationValue = await _organizationRepository.GetAsync(id);
            if (organizationValue.HasNoValue)
                throw new ServiceException($"Desired organization does not exist, id: {id}");
            var organization = organizationValue.Value;

            if (IsOwner(organization, userId) == false)
                throw new ServiceException($"User: {userId} is not allowed" + 
                    $"to update organization: {organization.Id}");

            organization.SetName(name);
            await _organizationRepository.UpdateAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationUpdated(organization.Id));
        }

        public async Task CreateAsync(string userId, string name, string description = "")
        {
            if (name.Empty())
                throw new ServiceException("Organization name can not be empty.");

            var userValue = await _userRepository.GetAsync(userId);
            if (userValue.HasNoValue)
                throw new ServiceException($"User has not been found for given id: '{userId}'.");

            var organizationValue = await _organizationRepository.GetAsync(name, userId);
            if (organizationValue.HasValue)
                throw new ServiceException($"There's already an organization with name: '{name}' " +
                                           $"for user with id: '{userId}'.");

            var organization = new Organization(name, userValue.Value, description);
            await _organizationRepository.AddAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationCreated(organization.Id));
        }

        public async Task CreateDefaultAsync(string userId)
        {
            await CreateAsync(userId, DefaultName, $"{DefaultName} description.");
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            var ogranizationValue = await _organizationRepository.GetAsync(id);
            if (ogranizationValue.HasNoValue)
                throw new ServiceException($"Desired organization does not exist, id: {id}");
            var organization = ogranizationValue.Value;

            if (IsOwner(organization, userId) == false)
                throw new ServiceException($"User: {userId} is not allowed" +
                    $"to delete organization: {organization.Id}");

            await _organizationRepository.DeleteAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationDeleted(organization.Id));
        }

        public async Task AssignUserAsync(Guid organizationId, string email, string userId)
        {
            var organization = await GetAndCheckOwnershipAsync(organizationId, userId);
            var user = await GetUserAsync(email);

            organization.AddUser(user);
            await _organizationRepository.UpdateAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationUserAdded(organizationId, user.Id));
        }

        public async Task UnassignUserAsync(Guid organizationId, string email, string userId)
        {
            var organization = await GetAndCheckOwnershipAsync(organizationId, userId);
            var user = await GetUserAsync(email);

            organization.RemoveUser(user.ExternalId);
            await _organizationRepository.UpdateAsync(organization);
            await _eventDispatcher.DispatchAsync(new OrganizationUserRemoved(organizationId, user.Id));
        }

        private bool IsOwner(Organization organization, string userId)
        {
            return organization.OwnerId == userId;
        }

        private async Task<Organization> GetAndCheckOwnershipAsync(Guid organizationId, string userId)
        {
            var organizationValue = await _organizationRepository.GetAsync(organizationId);
            if (organizationValue.HasNoValue)
                throw new ServiceException($"Desired organization does not exist, id: {organizationId}");

            var organization = organizationValue.Value;
            if (IsOwner(organization, userId) == false)
                throw new ServiceException($"User: {userId} is not allowed" +
                    $"to assign users into organization: {organization.Id}");

            return organization;
        }

        private async Task<User> GetUserAsync(string email)
        {
            var userValue = await _userRepository.GetByEmailAsync(email);
            if (userValue.HasNoValue)
                throw new ServiceException($"User with email: {email} does not exist.");

            return userValue.Value;
        }
    }
}