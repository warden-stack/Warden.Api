using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Warden.Common.Tasks
{
    public class TasksModule : Module
    {
        private readonly Assembly _assembly;

        public TasksModule(Assembly assembly)
        {
            _assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TaskHandler>().As<ITaskHandler>();
            builder.RegisterAssemblyTypes(_assembly).As(typeof(ITask));
        }
    }
}