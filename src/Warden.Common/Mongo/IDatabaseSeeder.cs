using System.Threading.Tasks;

namespace Warden.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}