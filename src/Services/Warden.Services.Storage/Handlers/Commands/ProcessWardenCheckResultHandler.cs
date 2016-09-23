using System;
using System.Threading.Tasks;
using RawRabbit.vNext.Disposable;
using Warden.Common.DTO.Wardens;
using Warden.Services.Commands;
using Warden.Services.Events;
using Warden.Services.Storage.Rethink;

namespace Warden.Services.Storage.Handlers.Commands
{
    public class ProcessWardenCheckResultHandler : ICommandHandler<ProcessWardenCheckResult>
    {
        private readonly IBusClient _bus;
        private readonly IWardenCheckStorage _wardenCheckStorage;

        public ProcessWardenCheckResultHandler(IBusClient bus, IWardenCheckStorage wardenCheckStorage)
        {
            _bus = bus;
            _wardenCheckStorage = wardenCheckStorage;
        }

        public async Task HandleAsync(ProcessWardenCheckResult command)
        {
            Console.WriteLine("Storing check result...");
            var storage = new WardenCheckResultStorageDto
            {
                OrganizationId = command.OrganizationId,
                WardenId = command.WardenId,
                Result = command.Result,
                CreatedAt = DateTime.UtcNow
            };
            await _wardenCheckStorage.SaveAsync(storage);
            await _bus.PublishAsync(new WardenCheckResultProcessed(command));
        }
    }
}