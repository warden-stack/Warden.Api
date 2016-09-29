using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.WardenChecks.Domain;

namespace Warden.Services.WardenChecks.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Maybe<Organization>> GetAsync(Guid id);
        Task<Maybe<Organization>> GetAsync(string name, string ownerId);
        Task UpdateAsync(Organization organization);
        Task AddAsync(Organization organization);
        Task DeleteAsync(Organization organization);
    }
}