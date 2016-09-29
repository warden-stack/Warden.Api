using Warden.Services.Domain;

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

        //protected WatcherCheckResult(Watcher watcher, string description, bool isValid)
        //{
        //    if(watcher == null)
        //        throw new DomainException("Watcher can not be null.");

        //    Watcher = watcher;
        //    Description = description;
        //    IsValid = isValid;
        //}

        //public static WatcherCheckResult Valid(Watcher watcher, string description)
        //    => new WatcherCheckResult(watcher, description, true);

        //public static WatcherCheckResult Invalid(Watcher watcher, string description)
        //    => new WatcherCheckResult(watcher, description, false);
    }
}