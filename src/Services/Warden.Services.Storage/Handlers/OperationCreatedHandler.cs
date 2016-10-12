using System;
using System.Threading.Tasks;
using Warden.Common.Events;
using Warden.Common.Events.Operations;
using Warden.DTO.Operations;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class OperationCreatedHandler : IEventHandler<OperationCreated>
    {
        private readonly IOperationRepository _operationRepository;

        public OperationCreatedHandler(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task HandleAsync(OperationCreated @event)
        {
            var operation = new OperationDto
            {
                Id = Guid.NewGuid(),
                RequestId = @event.RequestId,
                UserId = @event.UserId,
                Origin = @event.Origin,
                Resource = @event.Resource,
                State = @event.State,
                Message = @event.Message,
                CreatedAt = @event.CreatedAt,
                UpdatedAt = @event.UpdatedAt
            };
            await _operationRepository.AddAsync(operation);
        }
    }
}