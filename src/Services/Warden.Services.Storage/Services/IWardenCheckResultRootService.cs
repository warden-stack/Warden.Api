using System;
using System.Threading.Tasks;

namespace Warden.Services.Storage.Services
{
    public interface IWardenCheckResultRootService
    {
        Task ValidateAndAddAsync(string userId, Guid organizationId, Guid wardenId,
            object checkResult, DateTime createdAt);
    }
}