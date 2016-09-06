using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Rethink
{
    public class RealTimeDataStorage : IRealTimeDataStorage
    {
        private readonly IRethinkDbManager _dbManager;

        public RealTimeDataStorage(IRethinkDbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task SaveAsync(WardenCheckResultDto check)
        {
            await _dbManager.SaveWardenCheckResultAsync(check);
        }
    }
}