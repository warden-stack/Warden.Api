using System;
using NLog;
using StackExchange.Redis;

namespace Warden.Common.Caching.Redis
{
    public class RedisDatabaseFactory : IRedisDatabaseFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly RedisSettings _redisSettings;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisDatabaseFactory(RedisSettings redisSettings)
        {
            _redisSettings = redisSettings;
            TryConnect();
        }

        private void TryConnect()
        {
            if (!_redisSettings.Enabled)
            {
                Logger.Warn("Connection to Redis server has been skipped (disabled).");

                return;
            }

            try
            {
                _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisSettings.ConnectionString);
                Logger.Info("Connection to Redis server has been established.");
            }
            catch (Exception ex)
            {
                Logger.Error("Could not connect to Redis server.");
                Logger.Error(ex);
            }
        }

        public IDatabase GetDatabase(int id = -1)
        {
            var database = _connectionMultiplexer?.GetDatabase(id) ?? new EmptyRedisDatabase();

            return database;
        }
    }
}