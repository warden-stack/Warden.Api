using System;
using System.Collections.Generic;
using Warden.DTO.Watchers;

namespace Warden.DTO.Wardens
{
    public class WardenDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<WatcherDto> Watchers { get; set; }
    }
}