using System.Threading.Tasks;

namespace Warden.Api.Core.Services
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}