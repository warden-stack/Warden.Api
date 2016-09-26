using System.Threading.Tasks;
using Warden.Common.Events;

namespace Warden.Api.Infrastructure.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(params T[] events) where T : IEvent;
    }
}