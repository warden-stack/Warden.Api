using System.Threading.Tasks;

namespace Warden.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}