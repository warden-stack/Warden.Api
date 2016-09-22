using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Warden.Common.Commands;

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
            Console.WriteLine("Pushing out check result via sockets...");
            _signalRService.SendCheckResultSaved(message.OrganizationId, message.WardenId, message.Result);
            await Task.CompletedTask;
        }
    }
}