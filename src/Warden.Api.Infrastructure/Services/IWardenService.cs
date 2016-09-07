using System;
using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(string organizationInternalId, Guid userId, string name);
    }
}