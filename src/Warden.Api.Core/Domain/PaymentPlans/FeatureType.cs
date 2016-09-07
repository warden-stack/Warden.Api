namespace Warden.Api.Core.Domain.PaymentPlans
{
    public enum FeatureType
    {
        Organizations = 1,
        UsersInOrganizations = 2,
        Wardens = 3,
        WardenSpawns = 4,
        Watchers = 5,
        WardenChecksPerDay = 6,
        WardenChecksRetentionDays = 7,
        Metrics = 8
    }
}