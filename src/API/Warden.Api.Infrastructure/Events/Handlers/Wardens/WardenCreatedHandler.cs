using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Wardens;

namespace Warden.Api.Infrastructure.Events.Handlers.Wardens
{
    public class WardenCreatedHandler : IEventHandler<WardenCreated>
    {
        public async Task HandleAsync(WardenCreated @event)
        {
        }
    }
}