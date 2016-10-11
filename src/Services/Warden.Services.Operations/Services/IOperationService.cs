using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Domain;

namespace Warden.Services.Operations.Services
{
    public interface IOperationService
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task CreateAsync(Guid requestId, string userId, string origin, string resource, DateTime createdAt);
        Task ProcessAsync(Guid requestId);
        Task RejectAsync(Guid requestId);
        Task CompleteAsync(Guid requestId);
    }
}