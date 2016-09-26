using System.Threading.Tasks;
using Warden.Common.Commands;

namespace Warden.Api.Core.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}