using System;
using Warden.Common.Domain;

namespace Warden.Services.Organizations.Domain
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