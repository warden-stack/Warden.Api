using Warden.Common.Domain;

namespace Warden.Services.WardenChecks.Domain
{
    public class WatcherCheckResult : IValidatable
    {
        public string WatcherName { get; set; }
        public string WatcherType { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }

        protected WatcherCheckResult()
        {
        }
    }
}