using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Domain.Common;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Organizations;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.DTO;
using Warden.Api.Infrastructure.DTO.Organizations;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserService userService,
            IOrganizationService organizationService) 
            : base(commandDispatcher, mapper, userService)
        {
            _organizationService = organizationService;
        }

        // GET api/organizations
        [Authorize]
        [HttpGet]
        public async Task<PagedResult<OrganizationDto>> Get()
        {
            var user = await GetCurrentUser();
            var organizations = await _organizationService.BrowseAsync(user.Id);

            return organizations;
        }

        // GET api/organizations/{guid}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<OrganizationDto> Get(Guid id)
        {
            var organization = await _organizationService.GetAsync(id);

            return organization;
        }

        // POST api/organizations
        [HttpPost]
        public async Task Post([FromBody] CreateOrganization request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}