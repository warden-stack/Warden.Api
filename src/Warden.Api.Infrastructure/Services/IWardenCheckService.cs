using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenCheckService
    {
        Task SaveAsync(WardenCheckResultDto check);
    }

    public class WardenCheckService : IWardenCheckService
    {
        private readonly IRealTimeDataStorage _realTimeDataStorage;

        public WardenCheckService(IRealTimeDataStorage realTimeDataStorage)
        {
            _realTimeDataStorage = realTimeDataStorage;
        }

        public async Task SaveAsync(WardenCheckResultDto check)
        {
            await _realTimeDataStorage.SaveAsync(check);
        }
    }
}