using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Settings;
using Warden.Api.Validation;
using Warden.Messages.Commands.Users;

namespace Warden.Api.Modules
{
    public class ResetPasswordModule : ModuleBase
    {
        public ResetPasswordModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            AppSettings appSettings)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "")
        {
            Post("", async args => await For<ResetPassword>()
                .Set(x => x.Endpoint = appSettings.ResetPasswordUrl)
                .OnSuccessAccepted()
                .DispatchAsync());

            Post("set-new", async args => await For<SetNewPassword>()
                .OnSuccessAccepted()
                .DispatchAsync());
        }
    }
}