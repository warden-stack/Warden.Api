namespace Warden.Services.Storage.Rethink
{
    public class RethinkDbSettings
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string TableName { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}