using System.Threading.Tasks;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Services.WardenChecks.Rethink;

namespace Warden.Services.WardenChecks.Handlers
{
    public class ProcessWardenCheckResultHandler : ICommandHandler<ProcessWardenCheckResult>
    {
        private readonly IWardenCheckStorage _wardenCheckStorage;

        public ProcessWardenCheckResultHandler(IWardenCheckStorage wardenCheckStorage)
        {
            _wardenCheckStorage = wardenCheckStorage;
        }

        public async Task HandleAsync(ProcessWardenCheckResult command)
        {
            var storage = new
            {
                Result = command.Result,
                WardenId = command.WardenId,
                OrganizationId = command.OrganizationId,
                CreatedAt = command.CreatedAt
            };
            await _wardenCheckStorage.SaveAsync(storage);
        }
    }
}