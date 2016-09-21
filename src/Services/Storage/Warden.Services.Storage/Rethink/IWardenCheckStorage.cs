using System;
using System.Threading.Tasks;
using Warden.Common.Dto.Wardens;

namespace Warden.Services.Storage.Rethink
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