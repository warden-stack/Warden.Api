using Warden.Common.Domain;

namespace Warden.Services.Features.Domain
{
    public class Feature : ValueObject<Feature>
    {
        public FeatureType Type { get; protected set; }
        public int Limit { get; protected set; }

        protected Feature(FeatureType type, int limit)
        {
            Type = type;
            Limit = limit;
        }

        protected override bool EqualsCore(Feature other) => Type == other.Type;

        protected override int GetHashCodeCore() => (int) Type;

        public static Feature Organizations(int limit) => new Feature(FeatureType.AddOrganization, limit);
        public static Feature UsersInOrganizations(int limit) => new Feature(FeatureType.AddUserToOrganization, limit);
        public static Feature Wardens(int limit) => new Feature(FeatureType.AddWarden, limit);
        public static Feature WardenSpawns(int limit) => new Feature(FeatureType.AddWardenSpawn, limit);
        public static Feature Watchers(int limit) => new Feature(FeatureType.AddWatcher, limit);

        public static Feature WardenChecks(int limit)
            => new Feature(FeatureType.AddWardenCheck, limit);

        public static Feature WardenChecksRetentionDays(int limit)
            => new Feature(FeatureType.WardenChecksRetentionDays, limit);

        public static Feature Metrics(int limit) => new Feature(FeatureType.AddMetrics, limit);
        public static Feature ApiKeys(int limit) => new Feature(FeatureType.AddApiKey, limit);
    }
}