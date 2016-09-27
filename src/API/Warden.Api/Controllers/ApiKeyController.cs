using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Api.Core.Storage;
using Warden.Common.Commands.ApiKeys;

namespace Warden.Api.Controllers
{
    [Route("api-keys")]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyStorage _apiKeyStorage;

        public ApiKeyController(ICommandDispatcher commandDispatcher,
            IMapper mapper,
            IUserProvider userProvider,
            IApiKeyStorage apiKeyStorage)
            : base(commandDispatcher, mapper, userProvider)
        {
            _apiKeyStorage = apiKeyStorage;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<string>> Browse()
        {
            var apiKeys = await _apiKeyStorage.BrowseAsync(CurrentUserId);

            return apiKeys.HasValue ? apiKeys.Value : new List<string>();
        }

        [Authorize]
        [HttpPost]
        public async Task Create(RequestNewApiKey request) =>
            await For(request)
                .Authorize()
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => Created(Url.Action("Browse"), new object()))
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