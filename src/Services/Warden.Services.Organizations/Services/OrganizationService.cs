using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Common.DTO.Organizations;
using Warden.Common.DTO.Users;
using Warden.Common.DTO.Wardens;
using Warden.Common.DTO.Watchers;
using Warden.Common.Extensions;
using Warden.Common.Types;
using Warden.Services.Domain;
using Warden.Services.Organizations.Domain;
using Warden.Services.Organizations.Repositories;

namespace Warden.Services.Organizations.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;

        public OrganizationService(
            IOrganizationRepository organizationRepository, 
            IUserRepository userRepository)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedResult<OrganizationDto>> BrowseAsync(string userId)
        {
            var organizationValues = await _organizationRepository.BrowseAsync(userId, string.Empty);
            var organizationDtos = organizationValues.Items.Select(MapToDto);
            var organizations = PagedResult<OrganizationDto>.From(organizationValues, organizationDtos);

            return organizations;
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(Guid id)
        {
            var organizationValue = await _organizationRepository.GetAsync(id);
            if (organizationValue.HasNoValue)
                return new Maybe<OrganizationDto>();

            return MapToDto(organizationValue.Value);
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
        }

        public async Task CreateDefaultAsync(string userId)
        {
            await CreateAsync(userId, DefaultOrganizationName, $"{DefaultOrganizationName} description.");
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
        }

        public async Task AssignUserAsync(Guid organizationId, string email, string userId)
        {
            var organization = await GetAndCheckOwnershipAsync(organizationId, userId);
            var user = await GetUserAsync(email);

            organization.AddUser(user);
            await _organizationRepository.UpdateAsync(organization);
        }

        public async Task UnassignUserAsync(Guid organizationId, string email, string userId)
        {
            var organization = await GetAndCheckOwnershipAsync(organizationId, userId);
            var user = await GetUserAsync(email);

            organization.RemoveUser(user.UserId);
            await _organizationRepository.UpdateAsync(organization);
        }

        public string DefaultOrganizationName => "My organization";

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

        private static OrganizationDto MapToDto(Organization organization)
            => new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name,
                Description = organization.Description,
                OwnerId = organization.OwnerId,
                AutoRegisterNewWarden = organization.AutoRegisterNewWarden,
                Users = organization.Users.Select(x => new UserInOrganizationDto
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Role = x.Role,
                    CreatedAt = x.CreatedAt
                }),
                Wardens = organization.Wardens.Select(x => new WardenDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    Name = x.Name,
                    Enabled = x.Enabled,
                    Watchers = x.Watchers.Select(w => new WatcherDto
                    {
                        Name = w.Name,
                        Type = w.Type
                    })
                })
            };
    }
}