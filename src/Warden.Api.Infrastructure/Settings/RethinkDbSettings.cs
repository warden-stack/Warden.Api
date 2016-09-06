namespace Warden.Api.Infrastructure.Settings
{
    public class RethinkDbSettings
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}