using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}