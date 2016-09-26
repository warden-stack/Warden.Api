using StackExchange.Redis;

namespace Warden.Api.Core.Redis
{
    public interface IRedisDatabaseFactory
    {
        IDatabase GetDatabase(int id = -1);
    }
}