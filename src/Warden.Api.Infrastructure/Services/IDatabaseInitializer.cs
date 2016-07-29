using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}