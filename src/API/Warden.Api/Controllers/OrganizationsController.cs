﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Common.DTO.Organizations;
using Warden.Api.Infrastructure.Services;
using Warden.Common.Commands.Organizations;
using Warden.Common.Commands.Users;

namespace Warden.Api.Controllers
{
    [Route("organizations")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserProvider userProvider,
            IOrganizationService organizationService) 
            : base(commandDispatcher, mapper, userProvider)
        {
            _organizationService = organizationService;
        }

        // GET api/organizations
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<OrganizationDto>> Get()
        {
            var user = await GetCurrentUser();
            var organizations = await _organizationService.BrowseAsync(user.Id);

            return organizations.Items;
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
        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] CreateOrganization request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();

        // PUT api/organizations/{guid}/edit
        [Authorize]
        [HttpPut("{id}/edit")]
        public async Task Put(OrganizationDto request) =>
            await For(Mapper.Map<EditOrganization>(request))
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();

        // DELETE api/organizations/{guid}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(Guid id) =>
            await For(new AssignIntoOrganization())
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
        
    }
}