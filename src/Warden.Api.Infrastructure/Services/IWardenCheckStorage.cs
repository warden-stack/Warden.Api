using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenCheckStorage
    {
        Task SaveAsync(WardenCheckResultStorageDto storage);
        Task EnableStreamAsync();
        void DisableStream();
        void SubscribeToStream(object subscriber, Action<WardenCheckResultStorageDto> action);
        void UnsubscribeFromStream(object subscriber);
        void RemoveAllStreamSubscribers();
    }
}