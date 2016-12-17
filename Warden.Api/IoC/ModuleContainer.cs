using Autofac;
using Warden.Api.IoC.Modules;
using Warden.Common.Caching;

namespace Warden.Api.IoC
{
    public class ModuleContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AuthenticationModule>();
            builder.RegisterModule<DispatcherModule>();
            builder.RegisterModule<MongoRepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<StorageModule>();
            builder.RegisterModule<FilterModule>();
            builder.RegisterModule<EventHandlersModule>();
            builder.RegisterModule<ValidatorModule>();
            builder.RegisterModule<InMemoryCacheModule>();
        }
    }
}