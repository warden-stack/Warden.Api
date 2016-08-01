﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.DTO;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Controllers
{
    public class WardensController : ControllerBase
    {
        public WardensController(ICommandDispatcher commandDispatcher, IMapper mapper) 
            : base(commandDispatcher, mapper)
        {
        }

        [HttpPost]
        public async Task Post([FromBody] WardenDto request) =>
            await For(MapTo<CreateWarden>(request))
                .ExecuteAsync(c => CommandDispatcher.DispatchAsync(c))
                .OnFailure(ex => StatusCode(400))
                .OnSuccess(c => StatusCode(200))
                .HandleAsync();
    }
}