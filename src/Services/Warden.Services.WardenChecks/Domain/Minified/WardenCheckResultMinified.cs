namespace Warden.Services.WardenChecks.Domain.Minified
{
    public class WardenCheckResultMinified
    {
        public bool v { get; set; }
        public WatcherCheckResultMinified r { get; set; }
        public long s { get; set; }
        public long c { get; set; }
        public long t { get; set; }
        public ExceptionInfoMinified e { get; set; }

        public WardenCheckResultMinified()
        {
        }

        public WardenCheckResultMinified(WardenCheckResult check)
        {
            v = check.IsValid;
            r = new WatcherCheckResultMinified(check.WatcherCheckResult);
            s = check.StartedAt.Ticks;
            c = check.CompletedAt.Ticks;
            t = check.ExecutionTime.Ticks;
            e = check.Exception != null ? new ExceptionInfoMinified(check.Exception) : null;
        }
    }
}