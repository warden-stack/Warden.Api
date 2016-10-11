using System;

namespace Warden.Common.Commands
{
    public class CommandDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Origin { get; set; }
        public string Resource { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommandDetails Copy() => new CommandDetails
        {
            Id = Id,
            Origin = Origin,
            Resource = Resource,
            CreatedAt = DateTime.UtcNow
        };
    }
}