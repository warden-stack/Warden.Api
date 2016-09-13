using System;
using System.Threading.Tasks;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenCheckService
    {
        Task SaveAsync(Guid organizationId, Guid wardenId, WardenCheckResultDto check);
    }
}