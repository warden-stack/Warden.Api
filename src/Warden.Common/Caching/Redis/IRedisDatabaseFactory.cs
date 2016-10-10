using StackExchange.Redis;

namespace Warden.Common.Caching.Redis
{
    public interface IRedisDatabaseFactory
    {
        IDatabase GetDatabase(int id = -1);
    }
}