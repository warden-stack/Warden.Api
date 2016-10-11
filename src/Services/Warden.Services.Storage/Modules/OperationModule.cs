using Warden.DTO.Operations;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(IOperationProvider operationProvider) : base("operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, OperationDto>
                (async x => await operationProvider.GetAsync(x.RequestId)).HandleAsync());
        }
    }
}