using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Warden.Shared.Messages;

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
            var configurationId = Guid.NewGuid().ToString();
            var token = Guid.NewGuid().ToString();
            var region = "EU";
            await _bus.Publish(new CreateWardenMessage(configurationId, token, region));
        }
    }
}