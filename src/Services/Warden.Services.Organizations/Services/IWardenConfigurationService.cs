using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Domain;

namespace Warden.Services.Organizations.Services
{
    public interface IWardenConfigurationService
    {
        Task CreateAsync(Guid id, object configuration);
        Task<Maybe<WardenConfiguration>> GetAsync(Guid id);
    }
}