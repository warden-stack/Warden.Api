using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Rethink
{
    public class RealTimeDataPusher : IRealTimeDataPusher
    {
        private readonly IRethinkDbManager _dbManager;
        private readonly ISignalRService _signalRService;

        public RealTimeDataPusher(IRethinkDbManager dbManager, 
            ISignalRService signalRService)
        {
            _dbManager = dbManager;
            _signalRService = signalRService;
        }

        public async Task StartPushingAsync()
        {
            var feed = await _dbManager.StreamWardenCheckResultChangesAsync();
            while (feed.IsOpen)
            {
                foreach (var value in feed)
                {
                    var storage = value.NewValue;
                    _signalRService.SendCheckResultSaved(storage.OrganizationId,
                        storage.WardenId, storage.Check);
                }
                await feed.MoveNextAsync();
            }
        }
    }
}