using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public interface IStorageClient
    {
        Task<Maybe<T>> GetAsync<T>(string endpoint) where T : class;
    }
}