using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Wardens;
using Warden.Api.Core.Types;

namespace Warden.Api.Core.Repositories
{
    public interface IWardenConfigurationRepository
    {
        Task<Maybe<WardenConfiguration>> GetAsync(Guid id);
        Task AddAsync(WardenConfiguration configuration);
    }
}