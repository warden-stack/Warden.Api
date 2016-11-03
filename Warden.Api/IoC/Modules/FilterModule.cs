using System.Reflection;
using Autofac;
using Warden.Api.Filters;
using Warden.Common.Types;
using Module = Autofac.Module;

namespace Warden.Api.IoC.Modules
{
    public class FilterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilterResolver>().As<IFilterResolver>();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsClosedTypesOf(typeof(IFilter<,>));
        }
    }
}