﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Services;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Controllers
{
    [Route("organizations/{organizationId}/wardens")]
    public class WardensController : ControllerBase
    {
        public WardensController(ICommandDispatcher commandDispatcher, 
            IMapper mapper,
            IUserProvider userProvider) 
            : base(commandDispatcher, mapper, userProvider)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task Post(Guid organizationId, [FromBody] CreateWarden request) =>
            await For(request)
                .ExecuteAsync(async c =>
                {
                    var user = await GetCurrentUser();
                    c.AuthenticatedUserId = user.Id;
                    c.OrganizationId = organizationId;

                    await CommandDispatcher.DispatchAsync(c);
                })
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(201))
                .HandleAsync();
    }
}