using System;
using RawRabbit.vNext;
using Warden.Common.Events;
using Warden.Services.Stats.Handlers.Events;

namespace Warden.Services.Stats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.Stats";
            var client = BusClientFactory.CreateDefault();
            client.SubscribeAsync<WardenCheckResultProcessed>(async (msg, context) =>
                new WardenCheckResultProcessedHandler().HandleAsync(msg));
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
