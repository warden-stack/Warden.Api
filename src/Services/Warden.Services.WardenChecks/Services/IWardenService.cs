using System;
using System.Threading.Tasks;

namespace Warden.Services.WardenChecks.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(Guid id, string name, Guid organizationId, string userId, bool enabled);
        Task<bool> HasAccessAsync(string userId, Guid organizationId, Guid wardenId);
    }
}