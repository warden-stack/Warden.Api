using System;
using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(Guid id, Guid organizationId, string userId, string name);
        Task<bool> HasAccessAsync(string userId, Guid organizationId, Guid wardenId);
    }
}