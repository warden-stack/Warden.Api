using System;
using Newtonsoft.Json;
using Warden.Services.WardenChecks.Domain;
using Warden.Common.Extensions;
using Warden.Common.Types;

namespace Warden.Services.WardenChecks.Services
{
    public class WardenCheckService : IWardenCheckService
    {
        public Maybe<WardenCheckResultRoot> ValidateAndParseResult(string userId, 
            Guid organizationId, Guid wardenId, object checkResult, DateTime createdAt)
        {
            if (checkResult == null)
                return new Maybe<WardenCheckResultRoot>();

            var serializedResult = JsonConvert.SerializeObject(checkResult);
            var result = JsonConvert.DeserializeObject<WardenCheckResult>(serializedResult);
            ValidateCheckResult(result);

            return new WardenCheckResultRoot
            {
                UserId = userId,
                Result = result,
                WardenId = wardenId,
                OrganizationId = organizationId,
                CreatedAt = createdAt
            };
        }

        private void ValidateCheckResult(WardenCheckResult check)
        {
            if (check.WatcherCheckResult == null)
            {
                throw new ArgumentNullException(nameof(check.WatcherCheckResult),
                    "Watcher check result can not be null.");
            }
            if (check.WatcherCheckResult.WatcherName.Empty())
            {
                throw new ArgumentException("Watcher name can not be empty.",
                    nameof(check.WatcherCheckResult.WatcherName));
            }
            if (check.WatcherCheckResult.WatcherType.Empty())
            {
                throw new ArgumentException("Watcher type can not be empty.",
                    nameof(check.WatcherCheckResult.WatcherType));
            }
        }
    }
}