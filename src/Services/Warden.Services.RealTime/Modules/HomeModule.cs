using Nancy;

namespace Warden.Services.RealTime.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => Response.AsJson(new { name = "Warden.Services.RealTime" }));
        }
    }
}