using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Warden.Services.RealTime.Commands;

namespace Warden.Services.RealTime.Handlers.Commands
{
    public class ProcessWardenCheckResultHandler : IHandleMessages<ProcessWardenCheckResult>
    {
        private readonly ISignalRService _signalRService;

        public ProcessWardenCheckResultHandler(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public async Task Handle(ProcessWardenCheckResult message)
        {
            _signalRService.SendCheckResultSaved(message.OrganizationId, message.WardenId, message.Result);
            await Task.CompletedTask;
        }
    }
}