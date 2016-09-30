using System.Reflection;
using Autofac;
using Warden.Api.Core.Filters;
using Warden.Common.Types;
using Module = Autofac.Module;

namespace Warden.Api.Core.IoC.Modules
{
    public class FilterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilterResolver>().As<IFilterResolver>();
            var coreAssembly = Assembly.Load(new AssemblyName("Warden.Api.Core"));
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(openGenericServiceType: typeof(IFilter<,>));
        }
    }
}