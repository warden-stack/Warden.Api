using System;

namespace Warden.Common.Commands
{
    public class SpawnWarden : ICommand
    {
        public Guid UserId { get; }
        public string ConfigurationId { get; }
        public string Token { get; }
        public string Region { get; }

        protected SpawnWarden()
        {
        }

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