using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warden.Common.Tasks
{
    public interface ITaskHandler
    {
        Task ExecuteTasksAsync(IEnumerable<ITask> tasks);
    }
}