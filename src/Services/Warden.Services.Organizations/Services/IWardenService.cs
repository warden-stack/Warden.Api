using System;
using System.Threading.Tasks;

namespace Warden.Services.Organizations.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(Guid id, string name, Guid organizationId, string userId, bool enabled);
        Task DeleteWardenAsync(Guid id, Guid organizationId, string userId);
        Task<bool> HasAccessAsync(string userId, Guid organizationId, Guid wardenId);
    }
}