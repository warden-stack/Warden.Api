using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Wardens;

namespace Warden.Services.WardenChecks.Services
{
    public interface IWardenCheckService
    {
        Task ProcessAsync(Guid organizationId, Guid wardenId, WardenCheckResultDto check);
    }
}