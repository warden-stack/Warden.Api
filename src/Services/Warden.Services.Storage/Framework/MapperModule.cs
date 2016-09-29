using Autofac;
using Warden.Services.Storage.Mappers;

namespace Warden.Services.Storage.Framework
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserMapper>().AsSelf();
            builder.RegisterGeneric(typeof(CollectionMapper<>));
        }
    }
}