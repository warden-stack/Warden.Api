using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Domain;

namespace Warden.Services.Operations.Repositories
{
    public interface IOperationRepository
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task AddAsync(Operation operation);
        Task UpdateAsync(Operation operation);
    }
}