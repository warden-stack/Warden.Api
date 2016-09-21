using System.Threading.Tasks;
using Warden.Api.Core.Events;

namespace Warden.Api.Infrastructure.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}