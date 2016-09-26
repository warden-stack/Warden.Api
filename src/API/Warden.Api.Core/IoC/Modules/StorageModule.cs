using Autofac;
using Warden.Api.Core.Auth0;
using Warden.Api.Core.Storage;

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
        }
    }
}