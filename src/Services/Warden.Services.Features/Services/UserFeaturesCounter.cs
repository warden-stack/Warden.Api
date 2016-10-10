using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Common.Caching;
using Warden.Services.Features.Domain;

namespace Warden.Services.Features.Services
{
    public class UserFeaturesCounter : IUserFeaturesCounter
    {
        private readonly ICache _cache;

        public UserFeaturesCounter(ICache cache)
        {
            _cache = cache;
        }

        public async Task SetEmptyUsageAsync(string userId, IEnumerable<Feature> features)
        {
            var featureUsages = FeatureUsages.Empty(features);
            await _cache.AddAsync(GetKey(userId), featureUsages);
        }

        public async Task<bool> CanUseAsync(string userId, FeatureType feature)
        {
            var features = await _cache.GetAsync<FeatureUsages>(GetKey(userId));

            return features.HasNoValue || features.Value.Features[feature].CanUse;
        }

        public async Task IncreaseUsageAsync(string userId, FeatureType feature)
        {
            var key = GetKey(userId);
            var features = await _cache.GetAsync<FeatureUsages>(key);
            if (features.HasNoValue)
                return;

            features.Value.Features[feature].Usage++;
            await _cache.AddAsync(key, features.Value);
        }

        public async Task ResetUsageAsync(string userId, FeatureType feature)
        {
            await SetUsageAsync(userId, feature, 0);
        }

        public async Task SetUsageAsync(string userId, FeatureType feature, int usage)
        {
            var key = GetKey(userId);
            var features = await _cache.GetAsync<FeatureUsages>(key);
            if (features.HasNoValue)
                return;

            features.Value.Features[feature].Usage = usage;
            await _cache.AddAsync(key, features.Value);
        }

        private static string GetKey(string userId) => $"{userId}-features";

        private class FeatureUsages
        {
            public ConcurrentDictionary<FeatureType, FeatureUsage> Features { get; } = new ConcurrentDictionary<FeatureType, FeatureUsage>();

            public static FeatureUsages Empty(IEnumerable<Feature> features)
            {
                var usages = new FeatureUsages();
                var featureTypes = Enum.GetValues(typeof(FeatureType)).Cast<FeatureType>();
                foreach (var featureType in featureTypes)
                {
                    usages.Features[featureType] = new FeatureUsage
                    {
                        Limit = features.First(x => x.Type == featureType).Limit
                    };
                }

                return usages;
            }
        }

        private class FeatureUsage
        {
            public int Limit { get; set; }
            public int Usage { get; set; }
            public bool CanUse => Usage < Limit;
        }
    }
}