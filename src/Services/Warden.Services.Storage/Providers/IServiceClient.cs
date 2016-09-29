using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Mappers;

namespace Warden.Services.Storage.Providers
{
    public interface IServiceClient
    {
        Task<Maybe<T>> GetAsync<T>(string url, string endpoint, IMapper<T> mapper) where T : class;
    }
}