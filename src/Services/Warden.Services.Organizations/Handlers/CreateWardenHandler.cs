using System;
using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Common.Events.Wardens;
using Warden.Services.Organizations.Services;

namespace Warden.Services.Organizations.Handlers
{
    public class CreateWardenHandler : ICommandHandler<CreateWarden>
    {
        private readonly IBusClient _bus;
        private readonly IWardenService _wardenService;

        public CreateWardenHandler(IBusClient bus, 
            IWardenService wardenService)
        {
            _bus = bus;
            _wardenService = wardenService;
        }

        public async Task HandleAsync(CreateWarden command)
        {
            await _wardenService.CreateWardenAsync(command.WardenId,
                command.Name, command.OrganizationId, command.UserId, command.Enabled);
            await _bus.PublishAsync(new WardenCreated(command.Request.Id, command.WardenId,
                command.Name, command.OrganizationId, command.UserId,
                DateTime.UtcNow, command.Enabled));
        }
    }
}