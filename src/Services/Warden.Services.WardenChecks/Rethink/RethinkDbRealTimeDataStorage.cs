using System.Threading.Tasks;
using Warden.Common.DTO.Wardens;

namespace Warden.Services.WardenChecks.Rethink
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