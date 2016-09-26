using Autofac;
using Warden.Api.Core.Auth0;
using Warden.Api.Core.Cache;
using Warden.Api.Core.Redis;
using Warden.Api.Core.Services;
using Warden.Api.Core.Storage;

namespace Warden.Api.Core.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationService>()
                .As<IOrganizationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WardenService>()
                .As<IWardenService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<SecuredRequestService>()
                .As<ISecuredRequestService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WardenCheckService>()
                .As<IWardenCheckService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PaymentPlanService>()
                .As<IPaymentPlanService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserPaymentPlanService>()
                .As<IUserPaymentPlanService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserFeaturesManager>()
                .As<IUserFeaturesManager>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ApiKeyService>()
                .As<IApiKeyService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<UserProvider>()
                .As<IUserProvider>()
                .InstancePerLifetimeScope();

            //TODO: Register RabbitMQ
            builder.RegisterType<WardenConfigurationService>()
                .As<IWardenConfigurationService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UniqueIdGenerator>()
                .As<IUniqueIdGenerator>()
                .SingleInstance();
        }
    }
}