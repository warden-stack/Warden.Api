using System.Threading.Tasks;
using Warden.Common.Caching;

namespace Warden.Services.Features.Services
{
    public class WardenChecksCounter : IWardenChecksCounter
    {
        private readonly ICache _cache;

        public WardenChecksCounter(ICache cache)
        {
            _cache = cache;
        }

        public async Task<bool> IsInitializedAsync(string userId)
        {
            var featureUsage = await _cache.GetAsync<WardenChecksUsage>(GetKey(userId));

            return featureUsage.HasValue;
        }

        public async Task InitializeAsync(string userId, int limit)
        {
            var featureUsage = new WardenChecksUsage
            {
                Limit = limit
            };
            await _cache.AddAsync(GetKey(userId), featureUsage);
        }

        public async Task<bool> CanUseAsync(string userId)
        {
            var featureUsage = await _cache.GetAsync<WardenChecksUsage>(GetKey(userId));

            return featureUsage.HasNoValue || featureUsage.Value.CanUse;
        }

        public async Task IncreaseUsageAsync(string userId)
        {
            var key = GetKey(userId);
            var usage = await _cache.GetAsync<WardenChecksUsage>(key);
            if (usage.HasNoValue)
                return;

            usage.Value.Usage++;
            await _cache.AddAsync(key, usage.Value);
        }

        public async Task<int> GetUsageAsync(string userId)
        {
            var usage = await _cache.GetAsync<WardenChecksUsage>(GetKey(userId));

            return usage.HasValue ? usage.Value.Usage : 0;
        }

        public async Task ResetUsageAsync(string userId)
        {
            await SetUsageAsync(userId, 0);
        }

        public async Task SetUsageAsync(string userId, int usage)
        {
            var key = GetKey(userId);
            var featureUsage = await _cache.GetAsync<WardenChecksUsage>(key);
            if (featureUsage.HasNoValue)
                return;

            featureUsage.Value.Usage = usage;
            await _cache.AddAsync(key, featureUsage.Value);
        }

        private static string GetKey(string userId) => $"{userId}:checks";

        private class WardenChecksUsage
        {
            public int Limit { get; set; }
            public int Usage { get; set; }
            public bool CanUse => Usage < Limit;
        }
    }
}