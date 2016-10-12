using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.Wardens;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class DeleteWardenHandler : ICommandHandler<DeleteWarden>
    {
        private readonly IBusClient _bus;
        private readonly IWardenService _wardenService;

        public DeleteWardenHandler(IBusClient bus, IWardenService wardenService)
        {
            _bus = bus;
            _wardenService = wardenService;
        }

        public async Task HandleAsync(DeleteWarden command)
        {
            await _wardenService.DeleteWardenAsync(command.WardenId, command.OrganizationId, command.UserId);
            await _bus.PublishAsync(new WardenDeleted(command.Request.Id, command.WardenId, command.OrganizationId));
        }
    }
}