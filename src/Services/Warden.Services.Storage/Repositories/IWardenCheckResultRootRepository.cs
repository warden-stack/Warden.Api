using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.DTO.Wardens;

namespace Warden.Services.Storage.Repositories
{
    public interface IWardenCheckResultRootRepository
    {
        Task<Maybe<PagedResult<WardenCheckResultRootDto>>> BrowseAsync(Guid organizationId,
            Guid wardenId, int page = 1, int results = 10);

        Task AddAsync(WardenCheckResultRootDto checkResult);
        Task DeleteAsync(WardenCheckResultRootDto checkResult);
    }
}