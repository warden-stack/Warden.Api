using Microsoft.Owin.Cors;
using Owin;

namespace Warden.Services.RealTime
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}