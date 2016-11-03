using Autofac;
using Warden.Api.Mongo.Repositories;
using Warden.Api.Repositories;

namespace Warden.Api.IoC.Modules
{
    public class MongoRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SecuredRequestRepository>()
                .As<ISecuredRequestRepository>()
                .InstancePerLifetimeScope();
        }
    }
}