using System.Threading.Tasks;
using Warden.Api.Core.Events.Wardens;

namespace Warden.Api.Infrastructure.Events.Handlers.Wardens
{
    public class WardenCreatedHandler : IEventHandler<WardenCreated>
    {
        public async Task HandleAsync(WardenCreated @event)
        {
        }
    }
}