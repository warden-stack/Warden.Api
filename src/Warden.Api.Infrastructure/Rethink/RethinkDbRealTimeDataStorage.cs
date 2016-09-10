using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Rethink
{
    public class RethinkDbRealTimeDataStorage : IRealTimeDataStorage
    {
        private readonly IWardenCheckStorage _wardenCheckStorage;

        public RethinkDbRealTimeDataStorage(IWardenCheckStorage wardenCheckStorage)
        {
            _wardenCheckStorage = wardenCheckStorage;
        }

        public async Task StoreAsync(WardenCheckResultStorageDto checkResult)
        {
            await _wardenCheckStorage.SaveAsync(checkResult);
        }
    }
}