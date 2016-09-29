using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Domain;

namespace Warden.Services.Organizations.Repositories
{
    public interface IWardenConfigurationRepository
    {
        Task<Maybe<WardenConfiguration>> GetAsync(Guid id);
        Task AddAsync(WardenConfiguration configuration);
    }
}