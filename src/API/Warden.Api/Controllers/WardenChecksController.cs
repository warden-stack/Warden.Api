using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Services;
using System.Linq;
using Warden.Common.Commands.WardenChecks;

namespace Warden.Api.Controllers
{
    [Route("organizations/{organizationId}/wardens/{wardenId}/checks")]
    public class WardenChecksController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public WardenChecksController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserProvider userProvider,
            IApiKeyService apiKeyService)
            : base(commandDispatcher, mapper, userProvider)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpPost]
        public async Task Post(Guid organizationId, Guid wardenId, [FromBody] SaveWardenCheck request) =>
            await For(request)
                .ExecuteAsync(c =>
                {
                    c.AuthenticatedUserId = CurrentUserId;
                    c.OrganizationId = organizationId;
                    c.WardenId = wardenId;

                    return CommandDispatcher.DispatchAsync(c);
                })
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(201))
                .HandleAsync();

        //TODO: Refactor and improve performance e.g. by storing API keys in cache
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var apiKeyHeader = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "X-Api-Key");
            if (string.IsNullOrWhiteSpace(apiKeyHeader.Key))
            {
                context.HttpContext.Response.StatusCode = 401;
                return;
            }
            var apiKey = await _apiKeyService.GetAsync(apiKeyHeader.Value);
            if (apiKey == null)
            {
                context.HttpContext.Response.StatusCode = 401;
                return;
            }
            CurrentUserId = apiKey.UserId;

            await next();
        }
    }
}