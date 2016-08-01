using System;
using System.Threading.Tasks;
using Autofac;
using Warden.Api.Core.Events;

namespace Warden.Api.Infrastructure.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            foreach (var @event in events)
            {
                if (@event == null)
                    throw new ArgumentNullException(nameof(@event), "Event can not be null.");

                var eventType = @event.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                object handler;
                _context.TryResolve(handlerType, out handler);

                if (handler == null)
                    return;

                await (Task)((dynamic)handler).HandleAsync(@event);
            }
        }
    }
}