using System;

namespace Warden.Services.Spawn.Commands
{
    public class SpawnWarden
    {
        public Guid UserId { get; }
        public string ConfigurationId { get; }
        public string Token { get; }
        public string Region { get; }

        public SpawnWarden(Guid userId, string configurationId,
            string token, string region)
        {
            UserId = userId;
            ConfigurationId = configurationId;
            Token = token;
            Region = region;
        }
    }
}