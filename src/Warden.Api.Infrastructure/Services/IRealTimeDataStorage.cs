using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IRealTimeDataStorage
    {
        Task StoreAsync(WardenCheckResultStorageDto storage);
    }
}