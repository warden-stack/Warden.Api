using System;

namespace Warden.Common.Commands.ApiKeys
{
    public class RequestNewApiKey : IFeatureRequestCommand
    {
        public Guid ApiKeyId { get; set; }
        public CommandDetails Details { get; set; }
        public string UserId { get; set; }
    }
}