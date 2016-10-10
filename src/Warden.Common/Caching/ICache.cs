using System;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Common.Caching
{
    public interface ICache
    {
        Task<Maybe<T>> GetAsync<T>(string key) where T : class;
        Task AddAsync(string key, object value, TimeSpan? expiry = null);
        Task DeleteAsync(string key);
    }
}