using System;
using System.Threading.Tasks;
using Warden.Services.WardenChecks.Domain;

namespace Warden.Services.WardenChecks.Services
{
    public interface IWardenCheckService
    {
        Task ProcessAsync(Guid organizationId, Guid wardenId, WardenCheckResult check);
    }
}