using System.Net.Http;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public interface IHttpClient
    {
        Task<Maybe<HttpResponseMessage>> GetAsync(string url, string endpoint);
    }
}