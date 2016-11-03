using Autofac;
using Warden.Api.Storage;

namespace Warden.Api.IoC.Modules
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

            builder.RegisterType<OperationStorage>()
                .As<IOperationStorage>()
                .InstancePerLifetimeScope();
        }
    }
}