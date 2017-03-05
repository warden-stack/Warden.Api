using System.Threading.Tasks;
using Warden.Messages.Commands;

namespace Warden.Api.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}