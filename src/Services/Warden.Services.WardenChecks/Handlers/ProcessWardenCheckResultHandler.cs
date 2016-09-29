using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Services.WardenChecks.Domain;
using Warden.Services.WardenChecks.Services;

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
            var serializedResult = JsonConvert.SerializeObject(command.Result);
            var result = JsonConvert.DeserializeObject<WardenCheckResult>(serializedResult);
            var storage = new WardenCheckResultStorage
            {
                Result = result,
                WardenId = command.WardenId,
                OrganizationId = command.OrganizationId,
                CreatedAt = command.CreatedAt
            };
            await _wardenCheckStorage.SaveAsync(storage);
        }
    }
}