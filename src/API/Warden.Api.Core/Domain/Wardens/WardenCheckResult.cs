using System;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Watchers;

namespace Warden.Api.Core.Domain.Wardens
{
    public class WardenCheckResult : IValidatable, ICompletable
    {
        public bool IsValid { get; protected set; }
        public WatcherCheckResult WatcherCheckResult { get; protected set; }
        public DateTime StartedAt { get; protected set; }
        public DateTime CompletedAt { get; protected set; }
        public TimeSpan ExecutionTime => CompletedAt - StartedAt;
        public ExceptionInfo Exception { get; protected set; }

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