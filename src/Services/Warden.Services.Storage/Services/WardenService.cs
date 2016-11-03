using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Common.Domain;
using Warden.DTO.Wardens;
using Warden.Services.Storage.Repositories;
using Warden.Common.Extensions;

namespace Warden.Services.Storage.Services
{
    public class WardenService : IWardenService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public WardenService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task CreateWardenAsync(Guid id, string name, Guid organizationId, 
            string userId, DateTime createdAt, bool enabled)
        {
            if (name.Empty())
                throw new DomainException("Can not add a warden without a name to the organization.");

            var organization = await _organizationRepository.GetAsync(organizationId);
            if (organization.HasNoValue)
                throw new ArgumentException($"Organization {organizationId} has not been found.");

            if (organization.Value.OwnerId != userId)
            {
                throw new ArgumentException($"User {userId} has no rights to access " +
                                            $"organization {organization.Value.Id}.");
            }

            var wardenExists = organization.Value.Wardens.Any(x => x.Name.EqualsCaseInvariant(name));
            if (wardenExists)
                throw new DomainException($"Warden with name: '{name}' has been already added.");

            organization.Value.Wardens.Add(new WardenDto
            {
                Id = id,
                Name = name,
                CreatedAt = createdAt,
                Enabled = enabled
            });
            await _organizationRepository.UpdateAsync(organization.Value);
        }

        public async Task DeleteWardenAsync(Guid id, Guid organizationId)
        {
            var organization = await _organizationRepository.GetAsync(organizationId);
            if (organization.HasNoValue)
                throw new ArgumentException($"Organization {organizationId} has not been found.");

            var warden = organization.Value.Wardens.FirstOrDefault(x => x.Id == id);
            if (warden == null)
                return;

            organization.Value.Wardens.Remove(warden);
            await _organizationRepository.UpdateAsync(organization.Value);
        }
    }
}