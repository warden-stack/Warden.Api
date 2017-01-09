using Nancy.Security;
using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Api.Validation;
using Warden.Common.Extensions;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Api.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider,
            IOperationStorage operationStorage)
            : base(commandDispatcher, validatorResolver, identityProvider, modulePath: "operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, OperationDto>
            (async x =>
            {
                var operation = await operationStorage.GetAsync(x.RequestId);
                if (operation.HasNoValue || operation.Value.UserId.Empty())
                    return operation;

                this.RequiresAuthentication();

                return operation.Value.UserId == CurrentUserId
                    ? operation
                    : new Maybe<OperationDto>();
            }).HandleAsync());
        }
    }
}