using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Services.Users.Shared.Commands;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Api.Modules
{
    public class AccountModule : ModuleBase
    {
        public AccountModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            IUserStorage userStorage)
            : base(commandDispatcher, validatorResolver, identityProvider)
        {
            Get("account", async args => await Fetch<GetAccount, UserDto>
                (async x => await userStorage.GetAsync(x.UserId)).HandleAsync());

            Get("usernames/{name}/available", async args => await Fetch<GetNameAvailability, AvailableResourceDto>
                (async x => await userStorage.IsNameAvailableAsync(x.Name)).HandleAsync());

            Put("account/name", async args => await For<ChangeUsername>()
                .OnSuccessAccepted("account")
                .DispatchAsync());

            Put("account/avatar", async args => await For<ChangeAvatar>()
                .OnSuccessAccepted("account")
                .DispatchAsync());

            Put("account/password", async args => await For<ChangePassword>()
                .OnSuccessAccepted("account")
                .DispatchAsync());
        }
    }
}