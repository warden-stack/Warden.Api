using System;

namespace Warden.Common.Commands.ApiKeys
{
    public class RequestNewApiKey : IFeatureRequestCommand
    {
        public Guid ApiKeyId { get; set; }
        public Request Request { get; set; }
        public string UserId { get; set; }
    }
}