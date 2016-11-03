using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Operations;

namespace Warden.Api.Storage
{
    public interface IOperationStorage
    {
        Task<Maybe<OperationDto>> GetAsync(Guid requestId);
    }
}