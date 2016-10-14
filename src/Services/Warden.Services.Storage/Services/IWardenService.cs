using System;
using System.Threading.Tasks;

namespace Warden.Services.Storage.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(Guid id, string name, Guid organizationId, 
            string userId, DateTime createdAt, bool enabled);

        Task DeleteWardenAsync(Guid id, Guid organizationId);
    }
}