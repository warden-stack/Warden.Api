using Warden.Api.Core.Domain;
using Warden.Api.Core.Domain.Watchers;

namespace Warden.Api.Infrastructure.DTO.Watchers
{
    public class WatcherDto
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public WatcherDto()
        {
        }

        public WatcherDto(Watcher watcher)
        {
            Name = watcher.Name;
            Type = watcher.Type.ToString().ToLowerInvariant();
        }
    }
}