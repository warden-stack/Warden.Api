using System.Threading.Tasks;

namespace Warden.Api.Framework.Tasks
{
    public interface ITask
    {
        Task ExecuteAsync();
    }
}