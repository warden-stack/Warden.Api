using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.WardenChecks.Domain;

namespace Warden.Services.WardenChecks.Services
{
    public interface IWardenCheckService
    {
        Task<Maybe<WardenCheckResultRoot>> ValidateAndParseResultAsync(string userId, 
            Guid organizationId, Guid wardenId, object checkResult, DateTime createdAt);
    }
}