using Autofac;
using System.Reflection;
using Warden.Services.Storage.Mappers;
using Module = Autofac.Module;

namespace Warden.Services.Storage.Framework
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MapperResolver>().As<IMapperResolver>();
            var coreAssembly = typeof(Startup).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(typeof(IMapper<>));
            builder.RegisterAssemblyTypes(coreAssembly).AsClosedTypesOf(typeof(ICollectionMapper<>));
        }
    }
}