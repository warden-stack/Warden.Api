namespace Warden.Common.Dto.Watchers
{
    public class WatcherCheckResultDto
    {
        public string WatcherName { get; set; }
        public string WatcherType { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }
    }
}