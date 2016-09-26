using System.Threading.Tasks;
using RawRabbit;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Common.Commands;

namespace Warden.Api.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IBusClient _bus;

        public CommandDispatcher(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
                throw new ServiceException("Command can not be null.");

            await _bus.PublishAsync(command);
        }
    }
}