using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Warden.Api.Hubs
{
    public class WardenHub : Hub
    {
        public override async Task OnConnected()
        {
            await base.OnConnected();
        }

        public override async Task OnReconnected()
        {
            await base.OnReconnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
        }
    }
}