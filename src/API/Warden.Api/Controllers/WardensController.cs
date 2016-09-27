using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
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
        public async Task Post(Guid organizationId, [FromBody] RequestCreateWarden request) =>
            await For(request)
                .ExecuteAsync(async c =>
                {
                    c.UserId = CurrentUserId;
                    c.OrganizationId = organizationId;

                    await CommandDispatcher.DispatchAsync(c);
                })
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(201))
                .HandleAsync();
    }
}