﻿using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;
using Warden.Common.Commands;
using Warden.Common.Commands.Organizations;

namespace Warden.Api.Infrastructure.Commands.Handlers
{
    public class EditOrganizationHandler : ICommandHandler<EditOrganization>
    {
        private readonly IOrganizationService _organizationService;

        public EditOrganizationHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task HandleAsync(EditOrganization command)
        {
            await _organizationService.UpdateAsync(command.Id, command.Name, command.AuthenticatedUserId);
        }
    }
}