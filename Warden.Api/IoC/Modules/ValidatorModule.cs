using System.Reflection;
using Autofac;
using Warden.Api.Validation;
using Module = Autofac.Module;

namespace Warden.Api.IoC.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ValidatorResolver>().As<IValidatorResolver>();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsClosedTypesOf(typeof(IValidator<>));
        }
    }
}