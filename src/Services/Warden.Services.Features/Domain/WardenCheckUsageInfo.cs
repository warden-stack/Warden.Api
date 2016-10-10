namespace Warden.Services.Features.Domain
{
    public class WardenCheckUsageInfo
    {
        public string UserId { get; set; }
        public int Usage { get; set; }
        public int Limit { get; set; }
    }
}