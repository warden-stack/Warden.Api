using System;
using System.Threading.Tasks;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenCheckService
    {
        Task SaveAsync(string organizationInternalId, string wardenInternalId, WardenCheckResultDto check);
    }

    public class WardenCheckService : IWardenCheckService
    {
        private readonly IRealTimeDataStorage _realTimeDataStorage;
        private readonly IOrganizationRepository _organizationRepository;

        public WardenCheckService(IRealTimeDataStorage realTimeDataStorage,
            IOrganizationRepository organizationRepository)
        {
            _realTimeDataStorage = realTimeDataStorage;
            _organizationRepository = organizationRepository;
        }

        public async Task SaveAsync(string organizationInternalId, string wardenInternalId, WardenCheckResultDto check)
        {
            await ValidateCheckResultAsync(organizationInternalId, wardenInternalId, check);
            var storage = new WardenCheckResultStorageDto
            {
                OrganizationId = organizationInternalId,
                WardenId = wardenInternalId,
                Check = check,
                CreatedAt = DateTime.UtcNow
            };
            await _realTimeDataStorage.SaveAsync(storage);
        }

        private async Task ValidateCheckResultAsync(string organizationInternalId,
            string wardenInternalId, WardenCheckResultDto check)
        {
            if (check == null)
                throw new ArgumentNullException(nameof(check), "Warden check can not be null.");
            if (check.Result == null)
            {
                throw new ArgumentNullException(nameof(check.Result),
                    "Watcher check can not be null.");
            }
            if (check.Result.WatcherName.Empty())
                throw new ArgumentException("Watcher name can not be empty.");
            if (check.Result.WatcherType.Empty())
                throw new ArgumentException("Watcher type can not be empty.");

            var organization = await _organizationRepository.GetAsync(organizationInternalId);
            if (organization.HasNoValue)
            {
                throw new InvalidOperationException("Organization has not been found " +
                                                    $"for id: {organizationInternalId}.");
            }

            var warden = organization.Value.GetWardenByInternalId(wardenInternalId);
            if (warden == null)
            {
                throw new InvalidOperationException("Warden has not been found " +
                                                    $"for id: {wardenInternalId}.");
            }
        }
    }
}