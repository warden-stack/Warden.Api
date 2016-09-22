using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Warden.Common.Commands;
using Warden.Common.DTO.Wardens;
using Warden.Common.Events;
using Warden.Services.Storage.Rethink;

namespace Warden.Services.Storage.Handlers.Commands
{
    public class ProcessWardenCheckResultHandler : IHandleMessages<ProcessWardenCheckResult>
    {
        private readonly IWardenCheckStorage _wardenCheckStorage;
        private readonly IBus _bus;

        public ProcessWardenCheckResultHandler(IWardenCheckStorage wardenCheckStorage,
            IBus bus)
        {
            _wardenCheckStorage = wardenCheckStorage;
            _bus = bus;
        }

        public async Task Handle(ProcessWardenCheckResult message)
        {
            Console.WriteLine("Storing check result...");
            var storage = new WardenCheckResultStorageDto
            {
                OrganizationId = message.OrganizationId,
                WardenId = message.WardenId,
                Result = message.Result,
                CreatedAt = DateTime.UtcNow
            };
            await _wardenCheckStorage.SaveAsync(storage);
            await _bus.Publish(new WardenCheckResultProcessed(message));
        }
    }
}