using System.Threading.Tasks;
using Rebus.Bus;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenSpawnService
    {
        Task CreateWardenAsync(string configuration);
    }

    public class WardenSpawnService : IWardenSpawnService
    {
        private readonly IBus _bus;

        public WardenSpawnService(IBus bus)
        {
            _bus = bus;
        }

        public async Task CreateWardenAsync(string configuration)
        {
            //TODO: Create project with service bus messages
            //await _bus.Publish(new CreateWardenMessage(configurationId, token));
        }
    }
}