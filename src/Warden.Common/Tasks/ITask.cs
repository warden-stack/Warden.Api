using System.Threading.Tasks;

namespace Warden.Common.Tasks
{
    public interface ITask
    {
        Task ExecuteAsync();
    }
}