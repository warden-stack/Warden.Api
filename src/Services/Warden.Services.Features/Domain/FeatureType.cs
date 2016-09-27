namespace Warden.Services.Features.Domain
{
    public enum FeatureType
    {
        AddOrganization = 1,
        AddUserToOrganization = 2,
        AddWarden = 3,
        AddWardenSpawn = 4,
        AddWatcher = 5,
        AddWardenCheck = 6,
        WardenChecksRetentionDays = 7,
        AddMetrics = 8,
        AddApiKey = 9
    }
}