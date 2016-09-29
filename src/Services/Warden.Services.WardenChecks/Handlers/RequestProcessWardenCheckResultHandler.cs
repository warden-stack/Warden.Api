using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.Wardens;
using Warden.Services.WardenChecks.Services;

namespace Warden.Services.WardenChecks.Handlers
{
    public class RequestProcessWardenCheckResultHandler : ICommandHandler<RequestProcessWardenCheckResult>
    {
        private readonly IBusClient _bus;
        private readonly IWardenCheckService _wardenCheckService;
        private readonly IWardenCheckStorage _wardenCheckStorage;

        public RequestProcessWardenCheckResultHandler(IBusClient bus,
            IWardenCheckService wardenCheckService,
            IWardenCheckStorage wardenCheckStorage)
        {
            _bus = bus;
            _wardenCheckService = wardenCheckService;
            _wardenCheckStorage = wardenCheckStorage;
        }

        public async Task HandleAsync(RequestProcessWardenCheckResult command)
        {
            var result = await _wardenCheckService.ValidateAndParseResultAsync(command.OrganizationId,
                command.WardenId, command.Result, command.CreatedAt);
            if(result.HasNoValue)
                return;

            await _wardenCheckStorage.SaveAsync(result.Value);
            await _bus.PublishAsync(new WardenCheckResultProcessed(command.UserId, command.OrganizationId,
                command.WardenId, result.Value, command.CreatedAt));
        }
    }
}