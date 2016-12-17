using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Api.Storage
{
    public interface IOperationStorage
    {
        Task<Maybe<OperationDto>> GetAsync(Guid requestId);
    }
}