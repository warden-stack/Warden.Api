using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Controllers
{
    [Route("values")]
    public class ValuesController : ControllerBase
    {
        public ValuesController(ICommandDispatcher commandDispatcher, 
            IMapper mapper,
            IUserProvider userProvider) 
            : base(commandDispatcher, mapper, userProvider)
        {
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/values/secured
        [Authorize]
        [HttpGet("secured")]
        public async Task<object> GetAuthorized()
        {
            var externalUserId = User?.Identity?.Name;
            var userId = await GetCurrentUser();

            return new {message = $"You are authorized, userId: {userId.Id}, externalId: {externalUserId}"};
        }

        // GET api/values/claims
        [Authorize]
        [HttpGet("claims")]
        public object Claims()
        {
            return User.Claims.Select(c =>
            new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
