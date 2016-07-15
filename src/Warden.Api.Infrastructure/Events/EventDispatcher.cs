using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Warden.Api.Core.Domain;
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

        //TODO: Find out how to get generic handler in Autofac
        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            foreach (var @event in events)
            {
                if (@event == null)
                    throw new ServiceException("Event can not be null.");

                var eventType = @event.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                object handler;
                _context.TryResolve(handlerType, out handler);

                if (handler == null)
                    return;

                var method = handler.GetType()
                    .GetRuntimeMethods()
                    .First(x => x.Name.Equals("HandleAsync"));
                await (Task)method.Invoke(handler, new object[] { @event });
            }
        }
    }
}