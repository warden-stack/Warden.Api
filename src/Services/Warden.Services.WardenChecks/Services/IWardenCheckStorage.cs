using System.Threading.Tasks;
using Warden.Services.WardenChecks.Domain;

namespace Warden.Services.WardenChecks.Services
{
    public interface IWardenCheckStorage
    {
        Task SaveAsync(WardenCheckResultRoot checkResult);
    }
}