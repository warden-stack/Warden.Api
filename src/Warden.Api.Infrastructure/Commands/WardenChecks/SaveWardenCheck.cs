using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.WardenChecks
{
    public class SaveWardenCheck : ICommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid WardenId { get; set; }
        public WardenCheckResultDto Check { get; set; }
    }

    public class SaveWardenCheckHandler : ICommandHandler<SaveWardenCheck>
    {
        private readonly IWardenCheckService _wardenCheckService;

        public SaveWardenCheckHandler(IWardenCheckService wardenCheckService)
        {
            _wardenCheckService = wardenCheckService;
        }

        public async Task HandleAsync(SaveWardenCheck command)
        {
            await _wardenCheckService.SaveAsync(command.WardenId, command.Check);
        }
    }
}