using System;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Wardens;
using Warden.Common.Types;

namespace Warden.Api.Core.Mongo.Repositories
{
    public interface IWardenConfigurationRepository
    {
        Task<Maybe<WardenConfiguration>> GetAsync(Guid id);
        Task AddAsync(WardenConfiguration configuration);
    }
}