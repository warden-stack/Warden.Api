using System.Threading.Tasks;

namespace Warden.Api.Core.Services
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}