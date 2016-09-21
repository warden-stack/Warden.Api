using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}