using System;
using System.Threading.Tasks;
using Warden.Services.Commands;

namespace Warden.Services.RealTime.Handlers.Commands
{
    public class ProcessWardenCheckResultHandler : ICommandHandler<ProcessWardenCheckResult>
    {
        private readonly ISignalRService _signalRService;

        public ProcessWardenCheckResultHandler(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public async Task HandleAsync(ProcessWardenCheckResult command)
        {
            Console.WriteLine("Pushing out check result via sockets...");
            _signalRService.SendCheckResultSaved(command.OrganizationId, command.WardenId, command.Result);
            await Task.CompletedTask;
        }
    }
}