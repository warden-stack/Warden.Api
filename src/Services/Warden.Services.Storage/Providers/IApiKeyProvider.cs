using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warden.Services.Storage.Providers
{
    public interface IApiKeyProvider
    {
        Task<IEnumerable<string>>  BrowseAsync(string userId);
    }
}