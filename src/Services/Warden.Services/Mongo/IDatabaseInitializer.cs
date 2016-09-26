using System.Threading.Tasks;

namespace Warden.Services.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}