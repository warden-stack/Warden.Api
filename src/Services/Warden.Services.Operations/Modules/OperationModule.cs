using Warden.Services.Operations.Domain;
using Warden.Services.Operations.Queries;
using Warden.Services.Operations.Services;

namespace Warden.Services.Operations.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(IOperationService operationService) : base("operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, Operation>
                (async x => await operationService.GetAsync(x.RequestId)).HandleAsync());
        }
    }
}