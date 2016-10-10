using Autofac;
using Warden.Api.Core.Storage;
using Warden.Common.Caching;

namespace Warden.Api.Core.IoC.Modules
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StorageClient>()
                .As<IStorageClient>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserStorage>()
                .As<IUserStorage>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApiKeyStorage>()
                .As<IApiKeyStorage>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InMemoryCache>()
                .As<ICache>()
                .SingleInstance();
        }
    }
}