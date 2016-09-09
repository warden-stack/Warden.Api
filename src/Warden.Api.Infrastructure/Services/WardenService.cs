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

        public async Task CreateWardenAsync(string internalOrganizationId, Guid userId, string name)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user.HasNoValue)
                throw new ArgumentException($"User {userId} has not been found.");

            var organization = await _organizationRepository.GetAsync(internalOrganizationId);
            if (organization.HasNoValue)
                throw new ArgumentException($"Organization {internalOrganizationId} has not been found.");

            if (organization.Value.OwnerId != userId)
            {
                throw new ArgumentException($"User {userId} has no rights to access " +
                                            $"organization {organization.Value.Id}.");
            }

            var internalId = _uniqueIdGenerator.Create();
            organization.Value.AddWarden(user.Value, name, internalId);
            await _organizationRepository.UpdateAsync(organization.Value);
            await _eventDispatcher.DispatchAsync(organization.Value.Events.ToArray());
        }

        public async Task<bool> HasAccessAsync(Guid userId, string internalOrganizationId, string internalWardenId)
        {
            var organization = await _organizationRepository.GetAsync(internalOrganizationId);

            return organization.HasValue &&
                   organization.Value.OwnerId == userId &&
                   organization.Value.Wardens.Any(x => x.InternalId == internalWardenId);
        }
    }
}