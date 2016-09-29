using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Services.WardenChecks.Repositories;

namespace Warden.Services.WardenChecks.Services
{
    public class WardenService : IWardenService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public WardenService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task CreateWardenAsync(Guid id, string name, Guid organizationId, string userId, bool enabled)
        {
            var organization = await _organizationRepository.GetAsync(organizationId);
            if (organization.HasNoValue)
                throw new ArgumentException($"Organization {organizationId} has not been found.");

            if (organization.Value.OwnerId != userId)
            {
                throw new ArgumentException($"User {userId} has no rights to access " +
                                            $"organization {organization.Value.Id}.");
            }

            organization.Value.AddWarden(id, userId, name, enabled);
            await _organizationRepository.UpdateAsync(organization.Value);
        }

        public async Task<bool> HasAccessAsync(string userId, Guid organizationId, Guid wardenId)
        {
            var organization = await _organizationRepository.GetAsync(organizationId);

            return organization.HasValue &&
                   organization.Value.OwnerId == userId &&
                   organization.Value.Wardens.Any(x => x.OwnerId == userId);
        }
    }
}