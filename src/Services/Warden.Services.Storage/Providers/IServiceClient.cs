using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Mappers;

namespace Warden.Services.Storage.Providers
{
    public interface IServiceClient
    {
        Task<Maybe<T>> GetAsync<T>(string url, string endpoint) where T : class;
        Task<Maybe<PagedResult<T>>> GetCollectionAsync<T>(string url, string endpoint) where T : class;
    }
}