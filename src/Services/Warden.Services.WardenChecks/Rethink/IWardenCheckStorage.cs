using System;
using System.Threading.Tasks;

namespace Warden.Services.WardenChecks.Rethink
{
    public interface IWardenCheckStorage
    {
        Task SaveAsync(object storage);
        Task EnableStreamAsync();
        void DisableStream();
        void SubscribeToStream(object subscriber, Action<object> action);
        void UnsubscribeFromStream(object subscriber);
        void RemoveAllStreamSubscribers();
    }
}