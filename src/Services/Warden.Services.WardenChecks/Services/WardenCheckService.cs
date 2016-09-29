using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Services.WardenChecks.Domain;
using Warden.Common.Extensions;
using Warden.Common.Types;
using Warden.Services.WardenChecks.Repositories;

namespace Warden.Services.WardenChecks.Services
{
    public class WardenCheckService : IWardenCheckService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public WardenCheckService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Maybe<WardenCheckResultRoot>> ValidateAndParseResultAsync(string userId, 
            Guid organizationId, Guid wardenId, object checkResult, DateTime createdAt)
        {
            if (checkResult == null)
                return new Maybe<WardenCheckResultRoot>();

            var serializedResult = JsonConvert.SerializeObject(checkResult);
            var result = JsonConvert.DeserializeObject<WardenCheckResult>(serializedResult);
            await ValidateCheckResultAsync(organizationId, wardenId, result);

            return new WardenCheckResultRoot
            {
                UserId = userId,
                Result = result,
                WardenId = wardenId,
                OrganizationId = organizationId,
                CreatedAt = createdAt
            };
        }

        private async Task ValidateCheckResultAsync(Guid organizationId,
            Guid wardenId, WardenCheckResult check)
        {
            if (check.WatcherCheckResult == null)
            {
                throw new ArgumentNullException(nameof(check.WatcherCheckResult),
                    "Watcher check result can not be null.");
            }
            //if (check.WatcherCheckResult.Watcher == null)
            //{
            //    throw new ArgumentNullException(nameof(check.WatcherCheckResult),
            //        "Watcher an not be null.");
            //}
            if (check.WatcherCheckResult.WatcherName.Empty())
                throw new ArgumentException("Watcher name can not be empty.");
            if (check.WatcherCheckResult.WatcherType.Empty())
                throw new ArgumentException("Watcher type can not be empty.");

            var organization = await _organizationRepository.GetAsync(organizationId);
            if (organization.HasNoValue)
            {
                throw new InvalidOperationException("Organization has not been found " +
                                                    $"for id: {organizationId}.");
            }

            var warden = organization.Value.GetWardenById(wardenId);
            if (warden == null)
            {
                throw new InvalidOperationException("Warden has not been found " +
                                                    $"for id: {wardenId}.");
            }
        }
    }
}