using System;
using Nancy;
using Warden.Api.Commands;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Common.Extensions;
using Warden.Common.Security;
using Warden.Messages.Commands.Users;

namespace Warden.Api.Modules
{
    public class AuthenticationModule : ModuleBase
    {
        public AuthenticationModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            IUserStorage userStorage,
            IOperationStorage operationStorage,
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
                    var operation = await operationStorage.GetUpdatedAsync(c.Request.Id);
                    if(operation.HasNoValue || !operation.Value.Success)
                    {
                        return HttpStatusCode.Unauthorized;
                    }

                    var session = await userStorage.GetSessionAsync(c.SessionId);
                    if (session.HasNoValue)
                    {
                        return HttpStatusCode.Unauthorized;
                    }

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
                .OnSuccessAccepted("account")
                .DispatchAsync());

            Post("sign-out", async args => await For<SignOut>()
                .OnSuccess(HttpStatusCode.NoContent)
                .DispatchAsync());
        }
    }
}