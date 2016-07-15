using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenService
    {
        Task CreateWardenAsync(string name);
    }
}