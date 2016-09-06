using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.DTO.Wardens;
using Warden.Api.Infrastructure.DTO.Watchers;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    [Route("wardens/{id}/checks")]
    public class WardensChecksController : ControllerBase
    {
        private readonly IWardenCheckService _wardenCheckService;

        public WardensChecksController(ICommandDispatcher commandDispatcher, 
            IMapper mapper, 
            IUserService userService,
            IWardenCheckService wardenCheckService) 
            : base(commandDispatcher, mapper, userService)
        {
            _wardenCheckService = wardenCheckService;
        }

        [HttpPost]
        public async Task Post([FromBody] WardenCheckResultDto request)
        {
            await _wardenCheckService.SaveAsync(request);

            Response.StatusCode = 201;
        }
    }
}