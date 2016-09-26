using System.Threading.Tasks;

namespace Warden.Services.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}