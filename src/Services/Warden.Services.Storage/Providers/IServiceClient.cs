using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public interface IServiceClient
    {
        Task<Maybe<T>> GetAsync<T>(string url, string endpoint) where T : class;
    }
}