using Warden.Api.Core.Domain;

namespace Warden.Api.Infrastructure.DTO
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