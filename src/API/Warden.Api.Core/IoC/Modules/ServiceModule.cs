using Autofac;
using Warden.Api.Core.Services;

namespace Warden.Api.Core.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationService>()
                .As<IOrganizationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WardenService>()
                .As<IWardenService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<SecuredRequestService>()
                .As<ISecuredRequestService>()
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