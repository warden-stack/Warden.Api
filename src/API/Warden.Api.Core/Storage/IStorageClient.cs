using System;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public interface IStorageClient
    {
        Task<Maybe<T>> GetAsync<T>(string endpoint) where T : class;

        Task<Maybe<T>> GetAsyncUsingCache<T>(string endpoint, string cacheKey = null, TimeSpan? expiry = null)
            where T : class;
    }
}