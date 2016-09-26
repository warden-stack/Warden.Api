using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Common.DTO.ApiKeys;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Controllers
{
    [Route("api-keys")]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyController(ICommandDispatcher commandDispatcher, 
            IMapper mapper,
            IUserProvider userProvider,
            IApiKeyService apiKeyService) 
            : base(commandDispatcher, mapper, userProvider)
        {
            _apiKeyService = apiKeyService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<string>> Browse()
        {
            return Enumerable.Empty<string>();

            //var userId = (await GetCurrentUser()).Id;
            //var apiKeys = await _apiKeyService.BrowseAsync(userId);

            //return apiKeys.Select(x => x.Key);
        }

        [Authorize]
        [HttpGet("/{id}")]
        public async Task<string> Get(Guid id)
        {
            var apiKey = await _apiKeyService.GetAsync(id);

            return apiKey?.Key;
        }

        [Authorize]
        [HttpPost]
        public async Task Create() =>
            await For(new CreateApiKey())
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => Created(Url.Action("Get", new { id = c.Id}), new object()))
                .HandleAsync();

        [Authorize]
        [HttpDelete]
        public async Task Delete([FromBody] DeleteApiKey request) =>
            await For(request)
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}