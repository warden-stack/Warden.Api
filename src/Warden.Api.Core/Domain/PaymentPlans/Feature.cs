namespace Warden.Api.Core.Domain.PaymentPlans
{
    public class Feature : ValueObject<Feature>
    {
        public FeatureType Type { get; }
        public int Limit { get; }
        public bool IsDailyLimit { get; }

        protected Feature(FeatureType type, int limit, bool isDailyLimit)
        {
            Type = type;
            Limit = limit;
            IsDailyLimit = isDailyLimit;
        }

        protected override bool EqualsCore(Feature other) => Type == other.Type;

        protected override int GetHashCodeCore() => GetHashCode();

        public static Feature Create(FeatureType type, int limit, bool isDailyLimit = false)
            => new Feature(type, limit, isDailyLimit);
    }
}