using System;

namespace Warden.Common.Commands.Wardens
{
    public class SpawnWarden : IAuthenticatedCommand
    {
        public string UserId { get; set;  }
        public string ConfigurationId { get; }
        public object Configuration { get; }
        public string Token { get; }
        public string Region { get; }

        protected SpawnWarden()
        {
        }

        public SpawnWarden(string userId, object configuration,
            string configurationId,
            string token, string region)
        {
            UserId = userId;
            Configuration = configuration;
            ConfigurationId = configurationId;
            Token = token;
            Region = region;
        }
    }
}