using Nancy;

namespace Warden.Services.Stats.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => Response.AsJson(new { name = "Warden.Services.Stats" }));
        }
    }
}