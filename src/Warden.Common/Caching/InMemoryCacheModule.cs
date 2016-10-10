using Autofac;
using Microsoft.Extensions.Caching.Memory;

namespace Warden.Common.Caching
{
    public class InMemoryCacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new MemoryCache(new MemoryCacheOptions()))
                .As<IMemoryCache>()
                .SingleInstance();

            builder.RegisterType<InMemoryCache>()
                .As<ICache>()
                .SingleInstance();
        }
    }
}