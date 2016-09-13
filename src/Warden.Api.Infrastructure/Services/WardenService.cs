using System;
using System.Linq;
using System.Threading.Tasks;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.Events;

namespace Warden.Api.Infrastructure.Services
{
    public class WardenService : IWardenService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUniqueIdGenerator _uniqueIdGenerator;
        private readonly IEventDispatcher _eventDispatcher;

        public WardenService(IUserRepository userRepository,
            IOrganizationRepository organizationRepository,
            IUniqueIdGenerator uniqueIdGenerator,
            IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _uniqueIdGenerator = uniqueIdGenerator;
            _eventDispatcher = eventDispatcher;
        }

        public async Task CreateWardenAsync(Guid id, Guid organizationId, Guid userId, string name)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue)
                throw new ArgumentException($"User {userId} has not been found.");

            var organization = await _organizationRepository.GetAsync(organizationId);
            if (organization.HasNoValue)
                throw new ArgumentException($"Organization {organizationId} has not been found.");

            if (organization.Value.OwnerId != userId)
            {
                throw new ArgumentException($"User {userId} has no rights to access " +
                                            $"organization {organization.Value.Id}.");
            }

            organization.Value.AddWarden(id, user.Value, name);
            await _organizationRepository.UpdateAsync(organization.Value);
            await _eventDispatcher.DispatchAsync(organization.Value.Events.ToArray());
        }

        public async Task<bool> HasAccessAsync(Guid userId, Guid organizationId, Guid wardenId)
        {
            var organization = await _organizationRepository.GetAsync(organizationId);

            return organization.HasValue &&
                   organization.Value.OwnerId == userId &&
                   organization.Value.Wardens.Any(x => x.OwnerId == userId);
        }
    }
}