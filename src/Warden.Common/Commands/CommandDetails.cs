using System;

namespace Warden.Common.Commands
{
    public class CommandDetails
    {
        public Guid Id { get; }
        public Guid ResourceId { get; }
        public string OriginUrl { get; }
        public string ResourceUrl { get; }

        protected CommandDetails()
        {
        }

        public CommandDetails(Guid resourceId, string originUrl, string resourceUrl)
        {
            Id = Guid.NewGuid();
            ResourceId = resourceId;
            OriginUrl = originUrl;
            ResourceUrl = resourceUrl;
        }
    }
}