using System;
using System.Threading.Tasks;
using Warden.Common.Events;

namespace Warden.Services.Stats.Handlers.Events
{
    public class WardenCheckResultProcessedHandler
    {
        public async Task Handle(WardenCheckResultProcessed message)
        {
            Console.WriteLine("Updating stats...");
            await Task.CompletedTask;
        }
    }
}