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

        public async Task PushAsync()
        {
            _dbManager.SubscribeToStream(this, x => _signalRService.SendCheckResultSaved(x.OrganizationId,
                x.WardenId, x.Check));
            await _dbManager.StreamAsync();
        }
    }
}