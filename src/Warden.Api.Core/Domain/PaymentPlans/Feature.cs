namespace Warden.Api.Core.Domain.PaymentPlans
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

        public static Feature Organizations(int limit) => new Feature(FeatureType.Organizations, limit);
        public static Feature UsersInOrganizations(int limit) => new Feature(FeatureType.UsersInOrganizations, limit);
        public static Feature Wardens(int limit) => new Feature(FeatureType.Wardens, limit);
        public static Feature WardenSpawns(int limit) => new Feature(FeatureType.WardenSpawns, limit);
        public static Feature Watchers(int limit) => new Feature(FeatureType.Watchers, limit);

        public static Feature WardenChecks(int limit)
            => new Feature(FeatureType.WardenChecks, limit);

        public static Feature WardenChecksRetentionDays(int limit)
            => new Feature(FeatureType.WardenChecksRetentionDays, limit);

        public static Feature Metrics(int limit) => new Feature(FeatureType.Metrics, limit);
    }
}