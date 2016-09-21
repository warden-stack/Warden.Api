using System.Threading.Tasks;
using Warden.Common.DTO.Wardens;

namespace Warden.Services.Storage.Rethink
{
    public interface IRealTimeDataStorage
    {
        Task StoreAsync(WardenCheckResultStorageDto checkResult);
    }
}