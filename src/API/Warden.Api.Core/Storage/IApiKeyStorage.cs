using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public interface IApiKeyStorage
    {
        Task<Maybe<string>> GetUserIdForApiKeyAsync(string apiKey);
        Task<Maybe<IEnumerable<string>>> BrowseAsync(string userId);
    }
}