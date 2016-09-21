using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Wardens;

namespace Warden.Api.Core.Types
{
    public interface IWardenConfigurationRepository
    {
        Task<Maybe<WardenConfiguration>> GetAsync(Guid id);
        Task AddAsync(WardenConfiguration configuration);
    }
}