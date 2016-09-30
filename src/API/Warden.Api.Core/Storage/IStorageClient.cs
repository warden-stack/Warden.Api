using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Api.Core.Domain.Common;
using Warden.Common.Types;

namespace Warden.Api.Core.Storage
{
    public interface IStorageClient
    {
        Task<Maybe<T>> GetAsync<T>(string endpoint) where T : class;

        Task<Maybe<T>> GetUsingCacheAsync<T>(string endpoint, string cacheKey = null, TimeSpan? expiry = null)
            where T : class;

        Task<Maybe<PagedResult<T>>> GetCollectionUsingCacheAsync<T>(string endpoint, string cacheKey = null,
            TimeSpan? expiry = null) where T : class;

        Task<Maybe<PagedResult<TResult>>> GetFilteredCollectionUsingCacheAsync<TResult, TQuery>(TQuery query,
            string endpoint, string cacheKey = null, TimeSpan? expiry = null)
            where TResult : class where TQuery : class, IPagedQuery;
    }
}