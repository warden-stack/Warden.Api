using System;

namespace Warden.Api.Core.Domain
{
    public abstract class Entity : IIdentifiable
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}