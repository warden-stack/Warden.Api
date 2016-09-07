using System;
using System.Threading.Tasks;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Repositories;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenCheckService
    {
        Task SaveAsync(string internalId, WardenCheckResultDto check);
    }

    public class WardenCheckService : IWardenCheckService
    {
        private readonly IRealTimeDataStorage _realTimeDataStorage;
        private readonly IWardenRepository _wardenRepository;
        private readonly IUniqueIdGenerator _uniqueIdGenerator;

        public WardenCheckService(IRealTimeDataStorage realTimeDataStorage,
            IWardenRepository wardenRepository,
            IUniqueIdGenerator uniqueIdGenerator)
        {
            _realTimeDataStorage = realTimeDataStorage;
            _wardenRepository = wardenRepository;
            _uniqueIdGenerator = uniqueIdGenerator;
        }

        public async Task SaveAsync(string internalId, WardenCheckResultDto check)
        {
            if (check == null)
                throw new ArgumentNullException(nameof(check), "Warden check can not be null.");
            if (check.WatcherCheckResult == null)
            {
                throw new ArgumentNullException(nameof(check.WatcherCheckResult),
                    "Watcher check can not be null.");
            }
            if (check.WatcherCheckResult.WatcherName.Empty())
                throw new ArgumentException("Watcher name can not be empty.");
            if (check.WatcherCheckResult.WatcherType.Empty())
                throw new ArgumentException("Watcher type can not be empty.");

            var wardenExists = await _wardenRepository.ExistsAsync(internalId);
            if (!wardenExists)
                throw new InvalidOperationException($"Warden has not been found for id: {internalId}.");

            check.Id = _uniqueIdGenerator.GenerateId();
            var storage = new WardenCheckResultStorageDto
            {
                WardenId = internalId,
                Check = check,
                CreatedAt = DateTime.UtcNow
            };

            await _realTimeDataStorage.SaveAsync(storage);
        }
    }
}