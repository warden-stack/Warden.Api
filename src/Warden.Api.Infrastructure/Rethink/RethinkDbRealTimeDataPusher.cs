using System.Threading.Tasks;
using Warden.Api.Infrastructure.Services;

namespace Warden.Api.Infrastructure.Rethink
{
    public class RethinkDbRealTimeDataPusher : IRealTimeDataPusher
    {
        private readonly IWardenCheckStorage _wardenCheckStorage;
        private readonly ISignalRService _signalRService;

        public RethinkDbRealTimeDataPusher(IWardenCheckStorage wardenCheckStorage,
            ISignalRService signalRService)
        {
            _wardenCheckStorage = wardenCheckStorage;
            _signalRService = signalRService;
        }

        public async Task PushAsync()
        {
            _wardenCheckStorage.SubscribeToStream(this, x => _signalRService.SendCheckResultSaved(x.OrganizationId,
                x.WardenId, x.Check));
            await _wardenCheckStorage.EnableStreamAsync();
        }
    }
}