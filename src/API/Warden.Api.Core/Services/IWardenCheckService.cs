using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Wardens;

namespace Warden.Api.Core.Services
{
    public interface IWardenCheckService
    {
        Task ProcessAsync(Guid organizationId, Guid wardenId, WardenCheckResultDto check);
    }
}