using Warden.Api.Commands;
using Warden.Api.Queries;
using Warden.Api.Services;
using Warden.Api.Storage;
using Warden.Common.Types;
using Warden.DTO.Operations;

namespace Warden.Api.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(ICommandDispatcher commandDispatcher,
            IIdentityProvider identityProvider,
            IOperationStorage operationStorage)
            : base(commandDispatcher, identityProvider, modulePath: "operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, OperationDto>
            (async x =>
            {
                var operation = await operationStorage.GetAsync(x.RequestId);
                if (operation.HasNoValue || operation.Value.UserId != CurrentUserId)
                    return new Maybe<OperationDto>();

                return operation;
            }).HandleAsync());
        }
    }
}