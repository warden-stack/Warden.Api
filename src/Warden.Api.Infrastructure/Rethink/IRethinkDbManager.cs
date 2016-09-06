using System.Threading.Tasks;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Rethink
{
    public interface IRethinkDbManager
    {
        Task SaveWardenCheckResultAsync(WardenCheckResultDto check);
        Task<Cursor<Change<WardenCheckResultDto>>> StreamWardenCheckResultChangesAsync();
    }
}