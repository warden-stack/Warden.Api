using System;
using System.Threading.Tasks;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IWardenRepository
    {
        Task<Maybe<Domain.Wardens.Warden>> GetAsync(Guid id);
        Task AddAsync(Domain.Wardens.Warden warden);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string internalId);
    }
}