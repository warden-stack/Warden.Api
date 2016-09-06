using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Rethink
{
    public class RealTimeDataPusher : IRealTimeDataPusher
    {
        private readonly IRethinkDbManager _dbManager;

        public RealTimeDataPusher(IRethinkDbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task StartPushingAsync()
        {
            var feed = await _dbManager.StreamWardenCheckResultChangesAsync();
            while (feed.IsOpen)
            {
                foreach (var value in feed)
                {
                    var check = value.NewValue;
                }
                await feed.MoveNextAsync();
            }
        }
    }
}