using System.Threading.Tasks;
using Warden.Services.WardenChecks.Domain.Minified;

namespace Warden.Services.WardenChecks.Repositories
{
    public interface IWardenCheckResultRootMinifiedRepository
    {
        Task AddAsync(WardenCheckResultRootMinified checkResult);
    }
}