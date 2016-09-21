using System;
using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(Guid id, Guid organizationId, Guid userId, string name);
        Task<bool> HasAccessAsync(Guid userId, Guid organizationId, Guid wardenId);
    }
}