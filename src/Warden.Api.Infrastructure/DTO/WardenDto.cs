using System;
using System.Collections.Generic;
using System.Linq;

namespace Warden.Api.Infrastructure.DTO
{
    public class WardenDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<WatcherDto> Watchers { get; set; }

        public WardenDto()
        {
        }

        public WardenDto(Core.Domain.Warden warden)
        {
            Id = warden.Id;
            Name = warden.Name;
            Enabled = warden.Enabled;
            CreatedAt = warden.CreatedAt;
            Watchers = warden.Watchers.Select(x => new WatcherDto(x));
        }
    }
}