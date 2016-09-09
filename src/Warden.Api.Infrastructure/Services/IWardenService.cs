using System;
using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(string internalOrganizationId, Guid userId, string name);
        Task<bool> HasAccessAsync(Guid userId, string internalOrganizationId, string internalWardenId);
    }
}