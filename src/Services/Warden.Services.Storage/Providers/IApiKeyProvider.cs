using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public interface IApiKeyProvider
    {
        Task<Maybe<IEnumerable<string>>> BrowseAsync(string userId);
    }
}