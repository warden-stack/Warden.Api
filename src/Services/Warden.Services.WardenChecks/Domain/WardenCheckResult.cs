using System;
using Warden.Common.Domain;

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
    }
}