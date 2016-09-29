using System;
using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Wardens;

namespace Warden.Services.RealTime.Handlers
{
    public class WardenCheckResultProcessedHandler : IEventHandler<WardenCheckResultProcessed>
    {
        private readonly ISignalRService _signalRService;

        public WardenCheckResultProcessedHandler(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public async Task HandleAsync(WardenCheckResultProcessed @event)
        {
            Console.WriteLine("SignalR...");
            _signalRService.SendCheckResultSaved(@event.OrganizationId, @event.WardenId, @event.Result);
            await Task.CompletedTask;
        }
    }
}