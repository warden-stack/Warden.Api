using System;
using System.Threading.Tasks;
using Warden.Common.Commands;
using Warden.Common.DTO.Wardens;
using Warden.Common.Events;
using Warden.Services.Storage.Rethink;

namespace Warden.Services.Storage.Handlers.Commands
{
    public class ProcessWardenCheckResultHandler
    {
        private readonly IWardenCheckStorage _wardenCheckStorage;

        public ProcessWardenCheckResultHandler(IWardenCheckStorage wardenCheckStorage)
        {
            _wardenCheckStorage = wardenCheckStorage;
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
            //await _bus.Publish(new WardenCheckResultProcessed(message));
        }
    }
}