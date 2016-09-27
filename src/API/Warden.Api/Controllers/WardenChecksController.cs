using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Common.Commands.WardenChecks;
using Warden.Common.Commands.Wardens;

namespace Warden.Api.Controllers
{
    [Route("organizations/{organizationId}/wardens/{wardenId}/checks")]
    public class WardenChecksController : ControllerBase
    {
        public WardenChecksController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserProvider userProvider)
            : base(commandDispatcher, mapper, userProvider)
        {
        }

        [HttpPost]
        public async Task Post(Guid organizationId, Guid wardenId, [FromBody] SaveWardenCheck request) =>
            await For(request)
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(
                    new ProcessWardenCheckResult(CurrentUserId, organizationId,
                        wardenId, request.Check, DateTime.UtcNow)))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(201))
                .HandleAsync();

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var apiKeyHeader = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "X-Api-Key");
            if (string.IsNullOrWhiteSpace(apiKeyHeader.Key))
            {
                context.HttpContext.Response.StatusCode = 401;
                return;
            }
            var userId = await UserProvider.GetUserIdForApiKeyAsync(apiKeyHeader.Value);
            if (userId == null)
            {
                context.HttpContext.Response.StatusCode = 401;
                return;
            }
            SetCurrentUserId(userId);

            await next();
        }
    }
}