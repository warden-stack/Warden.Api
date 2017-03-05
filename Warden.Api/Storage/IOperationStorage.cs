using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Operations;

namespace Warden.Api.Storage
{
    public interface IOperationStorage
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task<Maybe<Operation>> GetUpdatedAsync(Guid requestId);
    }
}