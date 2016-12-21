using System;
using Nancy;
using Warden.Api.Authentication;
using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Common.Extensions;
using Warden.Services.Users.Shared.Commands;

namespace Warden.Api.Modules
{
    public class AccountModule : ModuleBase
    {
        public AccountModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            IUserStorage userStorage,
            IJwtTokenHandler jwtTokenHandler,
            JwtTokenSettings jwtTokenSettings)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "")
        {
            Post("sign-in", async (ctx, p) => await For<SignIn>()
                .Set(c =>
                {
                    c.IpAddress = Request.UserHostAddress;
                    c.UserAgent = Request.Headers.UserAgent;
                })
                .SetResourceId(c => c.SessionId)
                .OnSuccess(async c =>
                {
                    var session = await userStorage.GetSessionAsync(c.SessionId);
                    if (session.HasNoValue)
                        return HttpStatusCode.Unauthorized;

                    return new
                    {
                        token = jwtTokenHandler.Create(session.Value.UserId),
                        sessionId = session.Value.Id,
                        sessionKey = session.Value.Key,
                        expiry = DateTime.UtcNow.AddDays(jwtTokenSettings.ExpiryDays).ToTimestamp()
                    };
                })
                .DispatchAsync());

            Post("sign-up", async args => await For<SignUp>()
                .DispatchAsync());

            Post("sign-out", async args => await For<SignOut>()
                .DispatchAsync());

            Put("account/password", async args => await For<ChangePassword>()
                .OnSuccessAccepted("account")
                .DispatchAsync());

            Put("account/name", async args => await For<ChangeUserName>()
                .OnSuccessAccepted("account")
                .DispatchAsync());
        }
    }
}