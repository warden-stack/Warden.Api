﻿using System.Linq;
using System.Threading.Tasks;
using Warden.Api.Core.Events.Wardens;
using Warden.Api.Infrastructure.Events;
using Warden = Warden.Api.Core.Domain.Wardens.Warden;

namespace Warden.Api.Infrastructure.Services
{
    public class WardenService : IWardenService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public WardenService(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task CreateWardenAsync(string name)
        {
            var warden = new Core.Domain.Wardens.Warden(name);
            await _eventDispatcher.DispatchAsync(warden.Events.ToArray());
        }
    }
}