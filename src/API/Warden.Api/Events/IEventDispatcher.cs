using System.Threading.Tasks;
using Warden.Common.Events;

namespace Warden.Api.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(params T[] events) where T : IEvent;
    }
}