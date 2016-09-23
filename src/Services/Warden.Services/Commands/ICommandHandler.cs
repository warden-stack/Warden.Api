using System.Threading.Tasks;

namespace Warden.Services.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}