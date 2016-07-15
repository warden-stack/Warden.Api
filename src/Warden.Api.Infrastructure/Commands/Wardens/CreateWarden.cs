using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Commands.Wardens
{
    public class CreateWarden : ICommand
    {
        public string Name { get; set; }
    }

    public class CreateWardenHandler : ICommandHandler<CreateWarden>
    {
        private readonly IWardenService _wardenService;

        public CreateWardenHandler(IWardenService wardenService)
        {
            _wardenService = wardenService;
        }

        public async Task HandleAsync(CreateWarden command)
        {
            await _wardenService.CreateWardenAsync(command.Name);
        }
    }
}