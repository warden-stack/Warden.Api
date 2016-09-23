using System;
using RawRabbit.vNext;
using Warden.Services.Extensions;
using Warden.Services.Stats.Handlers.Events;

namespace Warden.Services.Stats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.Stats";
            var client = BusClientFactory.CreateDefault();
            client.SubscribeEventAsync(new WardenCheckResultProcessedHandler());
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
