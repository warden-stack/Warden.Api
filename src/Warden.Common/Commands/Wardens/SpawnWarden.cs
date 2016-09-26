using System;

namespace Warden.Common.Commands.Wardens
{
    public class SpawnWarden : ICommand
    {
        public Guid UserId { get; }
        public object Configuration { get; }
        public string ConfigurationId { get; }
        public string Token { get; }
        public string Region { get; }

        protected SpawnWarden()
        {
        }

        public SpawnWarden(Guid userId, object configuration, 
            string configurationId, string token, string region)
        {
            UserId = userId;
            Configuration = configuration;
            ConfigurationId = configurationId;
            Token = token;
            Region = region;
        }
    }
}