using System;
using RawRabbit.vNext;
using Warden.Common.Commands;
using Warden.Services.Spawn.Handlers.Commands;

namespace Warden.Services.Spawn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.Spawn";
            var client = BusClientFactory.CreateDefault();
            client.SubscribeAsync<SpawnWarden>(async (msg, context) =>
                    new SpawnWardenHandler(client).HandleAsync(msg));
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
