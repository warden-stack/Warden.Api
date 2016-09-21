using System;
using System.Collections.Generic;
using Warden.Common.Dto.Watchers;

namespace Warden.Common.Dto.Wardens
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