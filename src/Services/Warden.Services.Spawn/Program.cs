using System;
using Warden.Services.Extensions;
using RawRabbit.vNext;
using Warden.Services.Spawn.Handlers.Commands;

namespace Warden.Services.Spawn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Warden.Services.Spawn";
            var client = BusClientFactory.CreateDefault();
            client.SubscribeCommandAsync(new SpawnWardenHandler(client));
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
