using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenCheckService
    {
        Task SaveAsync(Guid wardenId, WardenCheckResultDto check);
    }

    public class WardenCheckService : IWardenCheckService
    {
        private readonly IRealTimeDataStorage _realTimeDataStorage;

        public WardenCheckService(IRealTimeDataStorage realTimeDataStorage)
        {
            _realTimeDataStorage = realTimeDataStorage;
        }

        public async Task SaveAsync(Guid wardenId, WardenCheckResultDto check)
        {
            var storage = new WardenCheckResultStorageDto
            {
                WardenId = wardenId,
                Check = check,
                CreatedAt = DateTime.UtcNow
            };

            await _realTimeDataStorage.SaveAsync(storage);
        }
    }
}