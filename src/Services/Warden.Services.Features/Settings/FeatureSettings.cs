namespace Warden.Services.Features.Settings
{
    public class FeatureSettings
    {
        public int MaxApiKeys { get; set; }
        public int MaxOrganizations { get; set; }
        public int MaxUsersInOrganization { get; set; }
        public int MaxWardensInOrganization { get; set; }
        public int MaxWatchersInWarden { get; set; }
        public int RetainWardenIterationDataDays { get; set; }
    }
}