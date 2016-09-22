using System;
using System.Threading.Tasks;
using Warden.Api.Core.Extensions;
using Warden.Api.Core.Repositories;
using Warden.Common.Commands;
using Warden.Common.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public class WardenCheckService : IWardenCheckService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public WardenCheckService(
            IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task ProcessAsync(Guid organizationId, Guid wardenId, WardenCheckResultDto check)
        {
            await ValidateCheckResultAsync(organizationId, wardenId, check);
            //await _bus.Publish(new ProcessWardenCheckResult(Guid.Empty, organizationId, wardenId, check));
        }

        private async Task ValidateCheckResultAsync(Guid organizationId,
            Guid wardenId, WardenCheckResultDto check)
        {
            if (check == null)
                throw new ArgumentNullException(nameof(check), "Warden check can not be null.");
            if (check.WatcherCheckResult == null)
            {
                throw new ArgumentNullException(nameof(check.WatcherCheckResult),
                    "Watcher check result can not be null.");
            }
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