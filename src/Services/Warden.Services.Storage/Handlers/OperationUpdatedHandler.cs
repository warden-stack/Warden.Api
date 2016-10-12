using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Operations;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class OperationUpdatedHandler : IEventHandler<OperationUpdated>
    {
        private readonly IOperationRepository _operationRepository;

        public OperationUpdatedHandler(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task HandleAsync(OperationUpdated @event)
        {
            var operation = await _operationRepository.GetAsync(@event.RequestId);
            if (operation.HasNoValue)
                return;

            operation.Value.Message = @event.Message;
            operation.Value.State = @event.State;
            operation.Value.UpdatedAt = @event.UpdatedAt;
            await _operationRepository.UpdateAsync(operation.Value);
        }
    }
}