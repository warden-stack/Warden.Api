namespace Warden.Common.Caching.Redis
{
    public class RedisSettings
    {
        public string ConnectionString { get; protected set; }
        public int Database { get; protected set; }
        public bool Enabled { get; protected set; }
    }
}