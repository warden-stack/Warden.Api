using System;

namespace Warden.Api.Core.Domain.Wardens
{
    public class WardenConfiguration : IdentifiableEntity
    {
        public string Configuration { get; protected set; }

        protected WardenConfiguration()
        {
        }

        public WardenConfiguration(Guid id, string configuration)
        {
            Id = id;
            Configuration = configuration;
        }
    }
}