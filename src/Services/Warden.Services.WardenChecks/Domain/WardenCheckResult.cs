using System;
using Warden.Services.Domain;

namespace Warden.Services.WardenChecks.Domain
{
    public class WardenCheckResult : IValidatable, ICompletable
    {
        public bool IsValid { get; set; }
        public WatcherCheckResult WatcherCheckResult { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public TimeSpan ExecutionTime => CompletedAt - StartedAt;
        public ExceptionInfo Exception { get; set; }

        protected WardenCheckResult()
        {
        }

        protected WardenCheckResult(bool isValid, WatcherCheckResult watcherCheckResult,
            DateTime startedAt, DateTime completedAt, ExceptionInfo exception = null)
        {
            IsValid = isValid;
            WatcherCheckResult = watcherCheckResult;
            StartedAt = startedAt;
            CompletedAt = completedAt;
            Exception = exception;
        }

        public static WardenCheckResult Valid(WatcherCheckResult watcherCheckResult,
            DateTime startedAt, DateTime completedAt)
            => new WardenCheckResult(true, watcherCheckResult, startedAt, completedAt);

        public static WardenCheckResult Invalid(WatcherCheckResult watcherCheckResult,
            DateTime startedAt, DateTime completedAt, ExceptionInfo exception)
            => new WardenCheckResult(false, watcherCheckResult, startedAt, completedAt, exception);
    }
}