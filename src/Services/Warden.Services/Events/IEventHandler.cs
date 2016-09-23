using System.Threading.Tasks;

namespace Warden.Services.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}