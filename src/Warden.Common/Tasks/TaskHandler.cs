using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warden.Common.Tasks
{
    public class TaskHandler : ITaskHandler
    {
        public async Task ExecuteTasksAsync(IEnumerable<ITask> tasks)
        {
            var tasksToExecute = tasks.Select(x => Task.Factory.StartNew<Task>(async () => await x.ExecuteAsync(), TaskCreationOptions.LongRunning));

            await Task.WhenAll(tasksToExecute);
        }
    }
}