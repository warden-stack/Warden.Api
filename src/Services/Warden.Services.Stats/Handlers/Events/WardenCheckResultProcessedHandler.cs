using System;
using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Wardens;

namespace Warden.Services.Stats.Handlers.Events
{
    public class WardenCheckResultProcessedHandler : IEventHandler<WardenCheckResultProcessed>
    {
        public async Task HandleAsync(WardenCheckResultProcessed @event)
        {
            Console.WriteLine("Updating stats...");
            await Task.CompletedTask;
        }
    }
}