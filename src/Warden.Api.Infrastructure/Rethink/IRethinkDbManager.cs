using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Rethink
{
    public interface IRethinkDbManager
    {
        Task SaveAsync(WardenCheckResultStorageDto storage);
        Task StreamAsync();
        void SubscribeToStream(object subscriber, Action<WardenCheckResultStorageDto> action);
    }
}