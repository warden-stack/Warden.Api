using System.Threading.Tasks;
using Warden.Api.Core.Events;

namespace Warden.Api.Infrastructure.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(params T[] events) where T : IEvent;
    }
}